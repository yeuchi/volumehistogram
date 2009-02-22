// ------------------------------------------------------------------
//Module:			RotateImage
//
//Description:		The acutual histogram in display
//
//Input:			mouse over
//				
//Output:			color values, coordinate, count
//					spins images
//					dispatch event to highlight color value in original
//				
//Author(s):		- CT Yeung
//				
//History:
//31Aug08			binary tree and display completed			(cty)
//28Sep08			working on spin capability, need scaling & offset.	(cty)
//
// Copyright (c) 2009 C.T.Yeung

// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:

// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
// CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

// ------------------------------------------------------------------
package volumeHistogram.com
{
	import flash.display.Bitmap;
	import flash.display.BitmapData;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.geom.Rectangle;
	
	import mx.controls.Image;
	
	[Event(name="newColorInfo", type="flash.events.Event")]
	[Event(name="refresh", type="flash.events.Event")]
	public class RotateImage extends Image
	{
		public var histogram:Array;
		public var clrInFocus:uint;
		public var clrCount:uint;
		
		protected const INDEX_COLOR:int=0;
		protected const INDEX_COUNT:int=1;
		protected const INDEX_POSX:int=2;
		protected const INDEX_POSY:int=3;
		protected const INDEX_POSZ:int=4;
		
		protected var bmpPlot:BitmapData;
		
///////////////////////////////////////////////////////////////////////////////
// Construct		
		
		// --------------------------------------------------------------------
		// Construct
		// --------------------------------------------------------------------
		public function RotateImage() {
			this.addEventListener(MouseEvent.MOUSE_MOVE, onMouseMove);
			this.addEventListener(MouseEvent.MOUSE_OUT, onMouseOut);
		}
		
		// --------------------------------------------------------------------
		// --------------------------------------------------------------------
		public function isEmpty():Boolean
		{
			if (!histogram)
				return true;
			return false;
		}
		
///////////////////////////////////////////////////////////////////////////////
// Public 
		
		public function onInitXYZ():void
		{
			if ( bmpPlot )
				bmpPlot.dispose();
					
			bmpPlot = new BitmapData(this.width, this.height, false, 0xFFFFFFFF);
			for (var i:int=0; i<histogram.length; i++){
				var entry:Array = histogram[i];	
				var color:uint = entry[INDEX_COLOR];
				
				var r:Number = ( color & 0xFF0000 ) >> 16;
				var g:Number = ( color & 0xFF00 ) >> 8;
				var b:Number = ( color & 0xFF );
				entry.push(r-128);
				entry.push(g-128);
				entry.push(b-128);
			}
			rotateZ(45);
			rotateY(45);
		}
		
		public function draw():Boolean
		{
			if ( bmpPlot )
				bmpPlot.dispose();
					
			bmpPlot = new BitmapData(this.width, this.height, false, 0xFFFFFFFF);
			for (var i:int=0; i<histogram.length; i++){
				var entry:Array = histogram[i];	
				var color:uint = entry[INDEX_COLOR];
				var x:Number = entry[INDEX_POSX];
				var y:Number = entry[INDEX_POSY];
				var z:Number = entry[INDEX_POSZ];
				
				bmpPlot.fillRect(new Rectangle(x+128,y+128,1,1), color);
			}
			source = new Bitmap(bmpPlot);
			return true;
		}
		
		// --------------------------------------------------------------------
		// --------------------------------------------------------------------
		public function rotateX(degree:Number):Boolean
		{
			var y:Number;
			var z:Number;
			var angle:Number = radian(degree);
			for ( var i:int = 0; i < histogram.length; i ++ )
			{
				var entry:Array = histogram[i];
				y = entry[INDEX_POSY];
				z = entry[INDEX_POSZ];
				//histogram[i*4] = 1*histogram[i*4];
				entry[INDEX_POSY] = Math.cos(angle)*y-Math.sin(angle)*z;
				entry[INDEX_POSZ] = Math.sin(angle)*y+Math.cos(angle)*z;
			}
			draw();
			return true;
		}
		
		// --------------------------------------------------------------------
		// --------------------------------------------------------------------
		public function rotateY(degree:Number):Boolean
		{
			var x:Number;
			var z:Number;
			
			var angle:Number = radian(degree);
			for ( var i:int = 0; i < histogram.length; i ++ )
			{
				var entry:Array = histogram[i];
				x = entry[INDEX_POSX];
				z = entry[INDEX_POSZ];
				entry[INDEX_POSX] = Math.cos(angle)*x+Math.sin(angle)*z;
				//m_XYZCVol[i*4+1] = m_XYZCVol[i*4+1];
				entry[INDEX_POSZ] = Math.cos(angle)*z-Math.sin(angle)*x;
			}
			draw();
			return true;
		}
		
		// --------------------------------------------------------------------
		// --------------------------------------------------------------------
		public function rotateZ(degree:Number):Boolean
		{
			var x:Number;
			var y:Number;
			
			var angle:Number = radian(degree);
			for ( var i:int = 0; i < histogram.length; i ++ )
			{
				var entry:Array = histogram[i];
				x = entry[INDEX_POSX];
				y = entry[INDEX_POSY];
				entry[INDEX_POSX] = Math.cos(angle)*x-Math.sin(angle)*y;
				entry[INDEX_POSY] = Math.sin(angle)*x+Math.cos(angle)*y;
				//m_XYZCVol[i*4+2] = m_XYZCVol[i*4+2];
			}
			draw();
			return true;
		}

/////////////////////////////////////////////////////////////////////
// Private
		
		// ------------------------------------------------------
		// draw reference x, y, z axis in the center
		// ------------------------------------------------------	
		private function drawAxis():void
		{
			
		}
		
		// ------------------------------------------------------
		// mouse hover over image, display histogram info.
		// - RGB value
		// - count
		// ------------------------------------------------------	
		private function onMouseMove(e:MouseEvent):void
		{
			clrCount = 0;
			clrInFocus = bmpPlot.getPixel(e.localX, e.localY);
			if ( clrInFocus !== 0xFFFFFF ) { 				// not white
				for ( var i:int=histogram.length-1; i>=0; i--) {
					var entry:Array = histogram[i];
					var clr:uint = entry[INDEX_COLOR];
					if ( clr == clrInFocus) {
						clrCount = entry[INDEX_COUNT];
						dispatchEvent(new Event("newColorInfo"));
						return;
					}
				}
			}
			dispatchEvent(new Event("refresh"));
		}
		
		private function onMouseOut(e:MouseEvent):void
		{
			dispatchEvent(new Event("refresh"));
		}
			
		protected function radian(degree:Number):Number
		{
			return Math.PI / 180 * degree;
		}
	}
}
