// ==================================================================
//	Module:			SketchBoard.as
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
	import flash.events.MouseEvent;
	import flash.events.TransformGestureEvent;
	import flash.geom.Point;
	import flash.geom.Vector3D;
	import flash.ui.MouseCursor;
	import flash.ui.Multitouch;
	import flash.ui.MultitouchInputMode;
	
	import mx.core.UIComponent;
	
	public class SketchBoard extends UIComponent
	{
		protected var mesh:Mesh;
		protected var pLast:Point;
		public var scale:Number = 1;
		public var xR:Number=0;
		public var yR:Number=0;
		public var zR:Number=0;
		protected var block:Boolean;
		
		// cross hair
		protected var vL:Vector3D;
		protected var vR:Vector3D;
		protected var vT:Vector3D;
		protected var vB:Vector3D;
		static protected const CROSS_HAIR_LEN:int= 30;
		
		public function SketchBoard()
		{
			super();
			mesh = new Mesh();
			pLast = new Point();
			
			// cross hair
			vL = new Vector3D(-CROSS_HAIR_LEN, 0, 0);
			vR = new Vector3D(CROSS_HAIR_LEN, 0, 0);
			vT = new Vector3D(0, -CROSS_HAIR_LEN, 0);
			vB = new Vector3D(0, CROSS_HAIR_LEN, 0);
			
			Multitouch.inputMode = MultitouchInputMode.GESTURE;
			this.addEventListener(TransformGestureEvent.GESTURE_ROTATE, onRotateZ, false, 0, true);
			this.addEventListener(TransformGestureEvent.GESTURE_ZOOM, onZoom, false, 0, true);
			this.addEventListener(MouseEvent.CLICK, onClick, false, 0, true);
		}
		
		public function undo():void {
			mesh.pop();
			if(mesh.length) 
				mesh.rotateAll(xR, yR, zR);
			render();
		}
		
		public function clear():void {
			mesh.dispose();
			xR = yR = zR = 0;
			pLast.x = pLast.y = 0;
			render();
		}
		
		public function rotateX(value:Number):void {
			xR += value;
			if(mesh.rotateAll(xR, yR, zR))
				render();
		}
		
		public function rotateY(value:Number):void {
			yR += value;
			if(mesh.rotateAll(xR, yR, zR))
			render();
		}
		
		// for now, we will consider this a multi-touch function
		// need to determine the correct axis to manipulate
		protected function onRotateZ(e:TransformGestureEvent):void {
			zR += e.rotation;
			if(mesh.rotateAll(xR, yR, zR))
			render();
			
			block = true;
		}
		
		public function render():void {
			init();
			
			var num:int = mesh.length;
			var p:Point = new Point();
			for(var i:int=0; i<num; i++) {
				p.x = mesh.vout[i*3]*scale+width/2.0;
				p.y = mesh.vout[i*3+1]*scale+height/2.0;
				
				this.graphics.lineStyle(1, 0xFF0000);
				this.graphics.drawCircle(p.x, p.y, 2);
				
				if(i>0) {
					this.graphics.lineStyle(1, 0xFF00);
					this.graphics.moveTo(pLast.x, pLast.y);
					this.graphics.lineTo(p.x, p.y);
				}
				pLast.x = p.x;
				pLast.y = p.y;
			}
		}
		
		protected function onZoom(e:TransformGestureEvent):void {
			scale *= e.scaleX;
			render();
			
			block = true;
		}
		
		public function init():void {
			// fill background
			this.graphics.beginFill(0xFFFFFF);
			this.graphics.drawRect(0,0,width, height);
			
			// draw cross-hair
			var vl:Vector3D = mesh.rotate(xR, yR, zR, vL);
			var vr:Vector3D = mesh.rotate(xR, yR, zR, vR);
			var vt:Vector3D = mesh.rotate(xR, yR, zR, vT);
			var vb:Vector3D = mesh.rotate(xR, yR, zR, vB);
			this.graphics.lineStyle(2, 0xFF);
			this.graphics.moveTo(vl.x*scale+width/2.0, vl.y*scale+height/2.0);
			this.graphics.lineTo(vr.x*scale+width/2.0, vr.y*scale+height/2.0);
			this.graphics.moveTo(vt.x*scale+width/2.0, vt.y*scale+height/2.0);
			this.graphics.lineTo(vb.x*scale+width/2.0, vb.y*scale+height/2.0);
			
			// draw center reference
			this.graphics.beginFill(0xFFFFFF);
			this.graphics.lineStyle(2, 0xFF);
			this.graphics.drawCircle(width/2, height/2, 10);
			this.graphics.endFill();
		}
		
		protected function onClick(e:MouseEvent):void {
			if(block) {
				block = false;
				return;
			}
			var v:Vector3D = new Vector3D(e.localX-width/2.0, 
										  e.localY-height/2.0, 
										  0);
			v = mesh.rotate(-xR, -yR, -zR, v);
			mesh.push(v.x, v.y, v.z);
			
			this.graphics.lineStyle(2, 0xFF0000);
			this.graphics.drawCircle(e.localX, e.localY, 4);
			
			if(mesh.length>1) {
				this.graphics.lineStyle(3, 0xFF00);
				this.graphics.moveTo(pLast.x, pLast.y);
				this.graphics.lineTo(e.localX, e.localY);
			}
			pLast.x = e.localX;
			pLast.y = e.localY;
		}
	}
}