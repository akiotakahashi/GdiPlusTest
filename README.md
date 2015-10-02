# GdiPlusTest

System.Drawing.Graphics.DrawImage has bugs with some InterpolationMode settings.

## Reproduction Steps

  1. Create an source image like ![src img](https://github.com/akiotakahashi/GdiPlusTest/raw/master/doc/fig-0.png)
  2. Call Graphics.DrawImage() 20 times to draw source image to destination with shifting 0.05 px to right
  3. For each InterpolationMode, do step 2
  4. Check the red line on the result

This repo is complete source files of a reproduction program.

## Problem Description

The image below is the result.

![Buggy DrawImage Results](https://github.com/akiotakahashi/GdiPlusTest/raw/master/doc/fig-1.png)

Three modes of InterpolationMode, High, HighQualityBilinear and HighQualityBicubic, draw incorrect vertical lines.

For easy understanding, we give you comparison below;
![Actual and Expected Result](https://github.com/akiotakahashi/GdiPlusTest/raw/master/doc/fig-2.png)

Clearly, the red line looks wrong.


It's confused that the horizontal lines of any InterpolationMode is correct.
Therefore it seems to be a bug of System.Drawing.Graphics.DrawImage.
