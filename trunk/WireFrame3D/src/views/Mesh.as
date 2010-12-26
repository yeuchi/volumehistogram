// ==================================================================
//	Module:			Mesh.as
//
//	Description:	Experiment of 3D rendering for mobile application.
//					This probably cannot be published.  
//					It might be considered conflict of interest as I am 
//					currently doing 3D rendering for Jostens.  However, it 
//					serves an excellent exercise for the new and exciting 
//					mobile development.  This maybe of value to Jostens at a 
//					later date.
//	
//	Author(s):		C.T. Yeung
//
//	History:			
//	25Dec10 		1st functional on Christmas!					
//					Although the rotation UI is some what buggy 	cty
// ==================================================================

package views
{
	import flash.geom.Matrix3D;
	import flash.geom.Vector3D;

	public class Mesh 
	{
		public var vector:Vector.<Number>;
		public var vout:Vector.<Number>;
		
		public function Mesh()
		{
			vector = new Vector.<Number>();
			vout = new Vector.<Number>();
		}
		
		public function dispose():void {
			while(vector.length)
				vector.pop();
			
			vout = null;
		}
		
		public function get length():int {
			return vector.length/3;
		}
		
		public function push(x:Number,
							 y:Number,
							 z:Number):void {
			vector.push(x);
			vector.push(y);
			vector.push(z);
		}
		
		public function pop():Vector3D {
			var z:Number=vector.pop();
			var y:Number=vector.pop();
			var x:Number=vector.pop();
			return new Vector3D(x,y,z);
		}
		
		public function rotate(x:Number,
							   y:Number,
							   z:Number,
							   v:Vector3D)
							   :Vector3D {
		   if(!x&&!y&&!z)
			   return v;
		  
		   var matrix:Matrix3D = getMatrix(x,y,z);
		   return (matrix)?matrix.transformVector(v):null;
		}
		
		protected function getMatrix(x:Number,
									 y:Number,
									 z:Number)
									:Matrix3D {
			var matrix:Matrix3D = new Matrix3D();
			if(x)
				matrix.appendRotation(x, Vector3D.X_AXIS);
			
			if(y)
				matrix.appendRotation(y, Vector3D.Y_AXIS);
			
			if(z)
				matrix.appendRotation(z, Vector3D.Z_AXIS);
			return matrix;
		}
									
		
		public function rotateAll(	x:Number,
							   		y:Number,
							   		z:Number)
									:Boolean {
			var matrix:Matrix3D = getMatrix(x,y,z);
			if(!matrix)
				return false;
			
			matrix.transformVectors(vector, vout);
			return true;
		}
	}
}