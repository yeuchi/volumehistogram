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
package volumeHistogram.com
{
	public class Matrix 
	{
		public var width:int=0;
		public var height:int=0;
		public var matrix:Array;
		
///////////////////////////////////////////////////////////////////////////////
// Construct		
		
		// --------------------------------------------------------------------
		// Construct
		// --------------------------------------------------------------------
		public function Matrix {
			
		}
		
		// --------------------------------------------------------------------
		// empty this object
		// --------------------------------------------------------------------
		public function empty():void
		{
			if (( matrix ) &&
				(matrix.length ))
				matrix.splice(0, matrix.length);
				
			width = 0;
			height = 0;
		}
		
		// --------------------------------------------------------------------
		// do we have a matrix
		// --------------------------------------------------------------------
		public function isEmpty():Boolean
		{
			if ((!matrix) || 
				(matrix.length == 0 ))
				return true;
			return false;
		}
		
///////////////////////////////////////////////////////////////////////////////
// Public 
		
		// --------------------------------------------------------------------
		// matrix multiplication
		// --------------------------------------------------------------------
		public function Multiply(source:Array):Array
		{
			if (( matrix == null ) ||
				( width != source.length ) ||
				( width <= 0 ) || ( height <= 0 ))
				return null;
				
			var destination:Array = new Array();
			
			var product:Number=0;
			for (var y:int=0; y<height; y++ ){
				for (var x:int=0; x<width; x++){
					product = source[x] * matrix[y*width+x];
				}
				destination.push(product);
				product = 0;
			}
			return destination;
		}
		
		// --------------------------------------------------------------------
		// --------------------------------------------------------------------
		public function Inverse():void
		{
			
		}
	    
	    // --------------------------------------------------------------------
	    // --------------------------------------------------------------------
	    public function RotateX():Array
	    {
	    	
	    }
	    
	    // --------------------------------------------------------------------
	    // --------------------------------------------------------------------
	    public function RotateY():Array
	    {
	    	
	    }
	    
	    // --------------------------------------------------------------------
	    // --------------------------------------------------------------------
	    public function RotateZ():Array
	    {
	    	
	    }
	}
}