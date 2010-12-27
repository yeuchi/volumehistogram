// ------------------------------------------------------------------
//Module:			RotateControl
//
//Description:		Buttons for spin
//
//Input:			mouse click
//				
//Output:			event to signal direction
//					- direction: left, right, up, down
//				
//Author(s):		- CT Yeung
//				
//History:
//31Aug08			binary tree and display completed			(cty)
//28Sep08			working on spin capability, need scaling & offset.	(cty)
//25Dec10			modified for WireFrame3D mobile development			(cty)
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
package views
{
	import flash.display.Bitmap;
	import flash.events.Event;
	import flash.events.MouseEvent;
	import flash.geom.Point;
	
	import mx.core.UIComponent;
	
	import spark.components.Image;

	
	[Event(name="rotateClicked", type="flash.events.Event")]
	
	public class RotateControl extends UIComponent
	{
		[Bindable][Embed(source="assets/arrowUp.png")]public var upClass:Class;
		[Bindable][Embed(source="assets/arrowDown.png")]public var downClass:Class;
		[Bindable][Embed(source="assets/arrowLeft.png")]public var leftClass:Class;
		[Bindable][Embed(source="assets/arrowRight.png")]public var rightClass:Class;
		static public const WIDTH:int = 200;
		protected var uiLeft:UIComponent;
		protected var uiRight:UIComponent;
		protected var uiUp:UIComponent;
		protected var uiDown:UIComponent;
		
		protected var center:Point;
		public var direction:String;
		protected var clickEvent:Event;
		
		static public const DIRECT_LEFT:String = "DIRECT_LEFT";
		static public const DIRECT_RIGHT:String = "DIRECT_RIGHT";
		static public const DIRECT_UP:String = "DIRECT_UP";
		static public const DIRECT_DOWN:String = "DIRECT_DOWN";

		public function RotateControl()
		{
			width = height = WIDTH;
			clickEvent = new Event("rotateClicked");
			onInitArrow(uiLeft, leftClass, DIRECT_LEFT, new Point(0, height/3));
			onInitArrow(uiRight, rightClass, DIRECT_RIGHT, new Point(width/3*2, height/3));
			onInitArrow(uiUp, upClass, DIRECT_UP, new Point(width/3, 0));
			onInitArrow(uiDown, downClass, DIRECT_DOWN, new Point(width/3, height/3*2));
		
			this.addEventListener(Event.ENTER_FRAME, onEnterFrame, false, 0, true); 
		}
		
		protected function onInitArrow(	ui:UIComponent,
							  			objClass:Class,
							  			id:String,
							  			pos:Point):void
		{
			var bmp:Bitmap = new objClass() as Bitmap;
			bmp.width*=2;
			bmp.height*=2;
			ui = new UIComponent();
			ui.width = bmp.width;
			ui.height = bmp.height;
			ui.addChild(bmp);
			ui.addEventListener(MouseEvent.ROLL_OUT, onRollOut);
			ui.addEventListener(MouseEvent.ROLL_OVER, onRollOver);
			ui.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
			ui.addEventListener(MouseEvent.MOUSE_UP, onRollOver);
			ui.id = id;
			ui.x = pos.x;
			ui.y = pos.y;
			this.addChild(ui);
		}		  
		
		protected var bDown:Boolean = false;
		protected function onEnterFrame(e:Event):void {
			if(bDown) 
				dispatchEvent(clickEvent);
		}
		
		protected function onMouseDown(e:MouseEvent):void
		{
			bDown = true;
			var ui:UIComponent = e.currentTarget as UIComponent;
			ui.alpha = .2;
			direction = ui.id;
		}
		
		protected function onRollOver(e:MouseEvent):void
		{
			bDown = false;
			var ui:UIComponent = e.currentTarget as UIComponent;
			ui.alpha = .8;
		}
		
		protected function onRollOut(e:MouseEvent):void
		{
			bDown = false;
			var ui:UIComponent = e.currentTarget as UIComponent;
			ui.alpha = 1;
		}
	}

}// ActionScript file
