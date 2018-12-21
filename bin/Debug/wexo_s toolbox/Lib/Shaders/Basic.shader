// OpenGL v3.3 and higher
#version 330

// Global preprocessor
#pragma debug(off)
#pragma optimize(on)

// Required values
#require HAS_NORMAL

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
#endif
	
	// Uniforms
	uniform mat4 uProjectionMatrix;
	uniform mat4 uModelViewMatrix;
	uniform mat4 uTransformationMatrix;
	
	// Shader starts here
	void main()
	{
		// Set the position
		gl_Position = uProjectionMatrix * uModelViewMatrix * vec4(InPosition.xyz, 1.0) * uTransformationMatrix;
		
		// Set the normal, if it exists
#ifdef HAS_NORMAL
		vfNormal = (vec4(InNormal.xyz, 1.0) * uModelViewMatrix).xyz;
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
	uniform vec3 uLightDirection;
#endif
	
	// Shader starts here
	void main()
	{
//		=====================
//		=== Ambient Color ===
//		=====================
		vec4 Ambient = uColor;

//		=====================================
//		=== Normal Calculation (Lighting) ===
//		=====================================
#ifdef HAS_NORMAL
		// Makes the normal readable + normalize it
		vec3 Normal = normalize(vfNormal);
		
		// Lighting
		float Brightness = dot(uLightDirection, Normal);
		Brightness = (Brightness + 2.0) / 4.0;
		Brightness = clamp(Brightness, 0.0, 1.0);
		vec4 LightColor = vec4(Brightness, Brightness, Brightness, 1.0);
#else
		// If no normals, keep the original color
		vec4 LightColor = vec4(1.0, 1.0, 1.0, 1.0);
#endif

//		-------------------------
//		=== Final Calculations ===
//		-------------------------

		// Calculates the color
		vec4 Color = Ambient * LightColor;

		// Stop processing if alpha = 0
		if (Color.w == 0)
		{
			discard;
		}

		// Sets the color of this fragment
		FragColor = Color;
	}

#end FRAGMENT