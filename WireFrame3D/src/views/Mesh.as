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
		protected var matrix:Matrix3D;
		
		public var x:Number;
		public var y:Number;
		public var z:Number;
		
		public function Mesh()
		{
			vector = new Vector.<Number>();
			vout = new Vector.<Number>();
			matrix = new Matrix3D();
			x = y = z = 0;
		}
		
		public function dispose():void {
			while(vector.length)
				vector.pop();
			
			while(vout.length)
				vout.pop();
			
			x = y = z = 0;
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
		
		public function rotate(xR:Number,
							   yR:Number,
							   zR:Number,
							   v:Vector3D)
							   :Vector3D {
		   if(!xR&&!yR&&!zR)
			   return v;
		  
		   var matrix:Matrix3D = new Matrix3D();
		   if(xR)
			   matrix.appendRotation(xR, Vector3D.X_AXIS);
		   
		   if(yR)
			   matrix.appendRotation(yR, Vector3D.Y_AXIS);
		   
		   if(zR)
			   matrix.appendRotation(zR, Vector3D.Z_AXIS);
		   
		   return matrix.transformVector(v);
		}
		
		public function appendRotation(	xR:Number,
									  	yR:Number,
									  	zR:Number)
										:void {
			if(xR)
				matrix.appendRotation(xR, Vector3D.X_AXIS);
			
			if(yR)
				matrix.appendRotation(yR, Vector3D.Y_AXIS);
			
			if(zR)
				matrix.appendRotation(zR, Vector3D.Z_AXIS);
			
			x += xR;
			y += yR;
			z += zR;
		}
		
		public function rotateAll():void {
			matrix.transformVectors(vector, vout);
		}
	}
}