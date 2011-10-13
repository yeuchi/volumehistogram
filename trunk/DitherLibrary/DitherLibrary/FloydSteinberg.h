#pragma once
#include "Bitmap.h"

class FloydSteinberg
{
public:
    FloydSteinberg(void);
    ~FloydSteinberg(void);

public:
    int apply(Bitmap *bmpSrc, Bitmap *bmpDes);

protected:
    //expandable to other bit depth for source & destination
    int bpp8to1(Bitmap *bmpSrc, Bitmap *bmpDes);
};
