// OpenGL v3.3 and higher
#version 330

// Global preprocessor
#pragma debug(off)
#pragma optimize(on)

// Required values
#require HAS_NORMAL
#require HAS_ALBEDO_MAP
#require HAS_NORMAL_MAP
#require HAS_VERTEX_COLOR

// ----------------------------------------------
// ----------------------------------------------
// ----------------------------------------------

// =============== Vertex Shader ===============
#start VERTEX

	// Inputs
	layout (location = 0) in vec3 InPosition;
	
#ifdef HAS_NORMAL
	layout (location = 1) in vec3 InNormal;
	out vec3 vfNormal;
	out vec3 uLightDirection;
	uniform vec3 uLightPosition;
	uniform vec3 uLightTarget;
#endif

#if defined(HAS_ALBEDO_MAP) || defined(HAS_NORMAL_MAP)
	layout (location = 2) in vec2 InUV;
	out vec2 vfUV;
#endif

#ifdef HAS_NORMAL_MAP
	layout (location = 4) in vec3 InTangent;
#endif
#ifdef HAS_VERTEX_COLOR
	layout (location = 5) in vec4 InVertexColor;
	out vec4 vfVertexColor;
#endif
	
	// Uniforms
	uniform mat4 uProjectionMatrix;
	uniform mat4 uModelViewMatrix;
	uniform mat4 uTransformationMatrix;
	
	// Shader starts here
	void main()
	{
		mat4 Transformation = uModelViewMatrix * uTransformationMatrix;
			
		// Set the position
		gl_Position = uProjectionMatrix * uModelViewMatrix * uTransformationMatrix * vec4(InPosition.xyz, 1.0);
		
		// Set the normal, if it exists
#ifdef HAS_NORMAL
		vec3 Normal = normalize((Transformation * vec4(InNormal.xyz, 0.0)).xyz);
		vfNormal = Normal;
		
		uLightDirection = normalize((vec4((uLightTarget - uLightPosition).xyz, 1.0) * Transformation).xyz);
#endif

		// Set the UV, if it exists
#if defined(HAS_ALBEDO_MAP) || defined(HAS_NORMAL_MAP)
		vfUV = InUV;
#endif
		
		// Normal map
#ifdef HAS_NORMAL_MAP
		//vec3 Normal = normalize((Transformation * vec4(InNormal.xyz, 0.0)).xyz);
		vec3 Tangent = normalize((Transformation * vec4(InTangent.xyz, 0.0)).xyz);
		vec3 Binormal = normalize(cross(Normal, Tangent)); // Does we really need to normalize this?
		
		mat3 ToTangentSpace = mat3
		(
			Tangent.x, Binormal.x, Normal.x,
			Tangent.y, Binormal.y, Normal.y,
			Tangent.z, Binormal.z, Normal.z
		);
		
		vec3 NormalMapLightTarget = ToTangentSpace * uLightTarget;
		vec3 NormalMapLightPosition = ToTangentSpace * uLightPosition;
		
		uLightDirection = normalize(NormalMapLightTarget - NormalMapLightPosition);
#endif

#ifdef HAS_VERTEX_COLOR
		vfVertexColor = InVertexColor;
#endif
	}

#end VERTEX

// ----------------------------------------------
// ----------------------------------------------
// ----------------------------------------------

// =============== Fragment Shader ===============
#start FRAGMENT

	// Outputs
	out vec4 FragColor;
	
	// Uniforms
	uniform vec4 uColor;
	
#ifdef HAS_NORMAL
	in vec3 vfNormal;
	in vec3 uLightDirection;
#endif

#ifdef HAS_VERTEX_COLOR
	in vec4 vfVertexColor;
#endif

#if defined(HAS_ALBEDO_MAP) || defined(HAS_NORMAL_MAP)
	in vec2 vfUV;
#endif

#ifdef HAS_ALBEDO_MAP
	uniform sampler2D Albedo0;
#endif
	
#ifdef HAS_NORMAL_MAP
	uniform sampler2D NormalMap;
#endif
	
	// Shader starts here
	void main()
	{
//		=====================
//		=== Ambient Color ===
//		=====================
		vec4 Ambient = uColor;
		
//		=====================================
//		=== Vertex Color Calculation ===
//		=====================================
#ifdef HAS_VERTEX_COLOR
		vec4 VertexColor = vfVertexColor;
#else
		vec4 VertexColor = vec4(1.0, 1.0, 1.0, 1.0);
#endif
		
//		=====================
//		=== Albedo0 Color ===
//		=====================
#ifdef HAS_ALBEDO_MAP
		// Gets the texture color
		vec4 AlbedoColor = texture(Albedo0, vfUV);
#else
		// If no texture, keep the original color
		vec4 AlbedoColor = vec4(1.0, 1.0, 1.0, 1.0);
#endif

		// Makes the normal readable
#if defined(HAS_NORMAL_MAP)
		vec3 Normal = 2.0 * texture(NormalMap, vfUV).rgb - 1.0;
		Normal.z = sqrt(1 - (Normal.x * Normal.x + Normal.y * Normal.y));
		Normal = normalize(Normal);
#elif defined(HAS_NORMAL)
		vec3 Normal = normalize(vfNormal);
#endif

//		=====================================
//		=== Normal Calculation (Lighting) ===
//		=====================================
#if defined(HAS_NORMAL) || defined(HAS_NORMAL_MAP)
		// Lighting
		float Brightness = dot(normalize(uLightDirection), Normal);
		Brightness = clamp((Brightness + 2.0) / 4.0 * 1.2, 0.0, 1.0);
		vec4 LightColor = vec4(Brightness, Brightness, Brightness, 1.0);
#else
		// If no normals, keep the original color
		vec4 LightColor = vec4(1.0, 1.0, 1.0, 1.0);
#endif

//		=========================
//		=== Final Calculations ===
//		=========================

		// Calculates the color
		vec4 Color = Ambient * VertexColor * AlbedoColor * LightColor;

		// Stop processing if alpha = 0
		if (Color.w == 0)
		{
			discard;
		}

		// Sets the color of this fragment
		FragColor = Color;
	}

#end FRAGMENT