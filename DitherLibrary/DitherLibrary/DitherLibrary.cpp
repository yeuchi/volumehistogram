// ==================================================================
// Module:      DitherLibrary.cpp : 
//
// Description: Defines the exported functions for the DLL application.
// 
// Reference:   http://blogs.msdn.com/b/jonathanswift/archive/2006/10/02/780637.aspx
// 
// Input:       clone()
//              - source and destination buffers
//
//              apply()
//              - method,
//              - source and destination buffers
//
// Output:      clone()
//
// ==================================================================

#include "stdafx.h"

// test my Bitmap class for cloning
extern "C" __declspec(dllexport) bool clone(unsigned char* bmpSrc,
                                            unsigned char* bmpDes) {
    
    return true;
}

// test dithering algorithm performance in C++
extern "C" __declspec(dllexport) int apply(char *szMethod, 
                                           char *bufSrc, 
                                           char *bufDes) {

    return 1;
}

