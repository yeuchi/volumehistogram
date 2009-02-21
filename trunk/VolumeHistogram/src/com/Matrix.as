package com
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