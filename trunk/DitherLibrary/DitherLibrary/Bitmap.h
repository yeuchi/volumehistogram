#pragma once

class Bitmap
{
public:
    Bitmap(void);
    Bitmap(char *szBuf, long lSize);
    Bitmap(long lWidth, long lHeight, int iDepth);
    ~Bitmap(void);

public:
    long getPixel(int x, int y);
    bool setPixel(int x, int y, long value);
};
