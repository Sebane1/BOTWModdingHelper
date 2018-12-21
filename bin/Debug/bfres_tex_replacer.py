#!/usr/bin/env python3
# -*- coding: utf-8 -*-

# BFRES textures replacer
# Copyright © 2018 AboodXD

import struct
import time
import sys
import os

includes = []
include_files = [r"C:\Users\stel9\AppData\Local\Programs\Python\Python37\DLLs\tcl86t.dll",
                 r"C:\Users\stel9\AppData\Local\Programs\Python\Python37\DLLs\tk86t.dll"]
os.environ['TCL_LIBRARY'] = r'C:\Users\stel9\AppData\Local\Programs\Python\Python37\tcl\tcl8.6'
os.environ['TK_LIBRARY'] = r'C:\Users\stel9\AppData\Local\Programs\Python\Python37\tcl\tk8.6'
base = 'Win32GUI' if sys.platform == 'win32' else None

from tkinter import tk, filedialog


def main():
    print("BFRES textures replacer")
    print("(C) 2018 AboodXD")

    root = Tk()
    root.withdraw()
    
    filetypes = [('BFRES files', '.bfres')]
    filename = filedialog.askopenfilename(filetypes=filetypes)

    if filename:
        with open(filename, "rb") as inf:
            inb = inf.read()

        if inb[:8] == b'FRES    ':
            print("\nSwitch BFRES detected!")

            filetypes2 = [('BNTX files', '.bntx')]
            filename2 = filedialog.askopenfilename(filetypes=filetypes2)

            bom = ">" if inb[0xC:0xE] == b'\xFE\xFF' else "<"

            endianness = {">": "Big", "<": "Little"}
            print("Endianness: " + endianness[bom])

            alignmentShift = inb[0xE]
            relocTbloff = struct.unpack(bom + "I", inb[0x18:0x1C])[0]
            relocTbl = bytearray(inb[relocTbloff:])

            inb = bytearray(inb[:relocTbloff])

            startoff = struct.unpack(bom + "q", inb[0x98:0xA0])[0]
            count = struct.unpack(bom + "q", inb[0xC8:0xD0])[0]

            if count in [0, -1]:
                print("\nNo Embedded files found.")
                time.sleep(5)
                exit(1)

            i = count - 1

            fileoff = struct.unpack(bom + "q", inb[startoff + i * 16:startoff + 8 + i * 16])[0]
            dataSize = struct.unpack(bom + "q", inb[startoff + 8 + i * 16:startoff + 16 + i * 16])[0]

            if inb[fileoff:fileoff + 4] != b'BNTX' or relocTbloff - fileoff - dataSize >= 1 << alignmentShift:
                print("\nCannnot replace the BNTX in this BFRES file, sadly.")
                time.sleep(5)
                exit(1)

            with open(filename2, "rb") as inf:
                inBNTX = inf.read()

            round_up = lambda x, y: ((x - 1) | (y - 1)) + 1

            inb[startoff + 8 + i * 16:startoff + 16 + i * 16] = struct.pack(bom + "q", len(inBNTX))
            inb[fileoff:] = inBNTX

            relocAlignBytes = b'\0' * (round_up(len(inb), 1 << alignmentShift) - len(inb))
            inb += relocAlignBytes

            bRelocTbloff = struct.pack(bom + "I", len(inb))
            relocTbl[4:8] = bRelocTbloff
            inb += relocTbl

            inb[0x18:0x1C] = bRelocTbloff
            inb[0x1C:0x20] = struct.pack(bom + "I", len(inb))

            with open(filename, "wb") as out:
                out.write(inb)

        else:
            print("\nUnable to recognize the input file!")
            time.sleep(5)
            exit(1)


if __name__ == '__main__':
    main()