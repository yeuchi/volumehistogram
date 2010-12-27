// ==================================================================
//	Module:			Mesh.as
//
//	Description:	Experiment of 3D rendering for mobile application.
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
		protected var matrix:Matrix3D;
		protected var mv:Vector.<Vector3D>;
		
		public function Mesh()
		{
			vector = new Vector.<Number>();
			vout = new Vector.<Number>();
			matrix = new Matrix3D();
			mv = matrix.decompose();
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
		  
		   setMatrix(x,y,z);
		   var v:Vector3D = matrix.transformVector(v);
		   matrix.recompose(mv);
		   return v;
		}
		
		protected function setMatrix(x:Number,
									 y:Number,
									 z:Number)
									:void {
			if(x)
				matrix.appendRotation(x, Vector3D.X_AXIS);
			
			if(y)
				matrix.appendRotation(y, Vector3D.Y_AXIS);
			
			if(z)
				matrix.appendRotation(z, Vector3D.Z_AXIS);
		}
									
		
		public function rotateAll(	x:Number,
							   		y:Number,
							   		z:Number)
									:Boolean {
			setMatrix(x,y,z);
			matrix.transformVectors(vector, vout);
			matrix.recompose(mv);
			return true;
		}
	}
}