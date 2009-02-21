// ------------------------------------------------------------------
//Module:			BinaryTree
//
//Description:		histogram of color values
//
//Input:			pixel color
//				
//Output:			a histogram list of color and count
//				
//Author(s):		- CT Yeung
//				
//History:
//31Aug08			binary tree and display completed			(cty)
//28Sep08			working on spin capability, need scaling & offset.	(cty)
// ------------------------------------------------------------------
package com
{
	import flash.geom.Point;
	
	public class BinaryTree
	{
		protected var leftBranch:BinaryTree;
		protected var rightBranch:BinaryTree;
		protected var color:uint;
		protected var iCount:int=0;
		
///////////////////////////////////////////////////////////////////////////////
// Construct

		// --------------------------------------------------------------------
		// Construct
		// --------------------------------------------------------------------
		public function BinaryTree()
		{
			
		}
		
		// --------------------------------------------------------------------
		// empty just this instance, not left nor right
		// --------------------------------------------------------------------
		public function emptyNode():void
		{
			color = 0;
			iCount = 0;
		}
		
		// --------------------------------------------------------------------
		// just this node...
		// --------------------------------------------------------------------
		public function isNodeEmpty():Boolean
		{
			if (iCount==0)
				return true;
			return false;
		}
		
		// --------------------------------------------------------------------
		// recursive delete
		// --------------------------------------------------------------------
		public function emptyTree():void
		{
			color = 0;
			iCount = 0;
			if ( leftBranch ) {
				leftBranch.emptyTree();
				leftBranch = null;
			}
			
			if ( rightBranch ) {
				rightBranch.emptyTree();
				rightBranch = null;
			}
		}

		// --------------------------------------------------------------------
		// is this tree empty ?
		// --------------------------------------------------------------------
		public function isTreeEmpty():Boolean
		{
			if (( iCount==0 ) &&
			   ( !leftBranch ) &&
			   ( !rightBranch ))
			   return true;
			   
		   return false;
		}

///////////////////////////////////////////////////////////////////////////////
// Public functions		
		
		// --------------------------------------------------------------------
		// recursive insert() function
		// --------------------------------------------------------------------
		public function insert(clr:uint):Boolean
		{
			if ( iCount == 0 ) {
				color = clr;
				iCount = 1;
				return true;
			}
				
			else if ( clr < color ) {
				if ( !leftBranch )
					leftBranch = new BinaryTree();
					
				return leftBranch.insert(clr);
			}
				
			else if ( clr > color ) {
				if (!rightBranch )
					rightBranch = new BinaryTree();
					
				return rightBranch.insert(clr);
			}
				
			iCount ++;
			return true;			
		}
		
		// --------------------------------------------------------------------
		// sorted by assending order... so 1st is the smallest
		// --------------------------------------------------------------------
		public function getFirst():uint
		{
			if (leftBranch)
				return leftBranch.getFirst();
			
			else if ( iCount > 0 )
				return color;
				
			else 
				return 0;
		}	
		
		// --------------------------------------------------------------------
		// sorted by assending order... so last is the largest
		// --------------------------------------------------------------------
		public function getLast():uint
		{
			if (rightBranch)
				return rightBranch.getLast();
				
			else if ( iCount > 0 )
				return color;
				
			else
				return 0;
		}
		
		// --------------------------------------------------------------------
		// Find this color entry in tree and return count
		// --------------------------------------------------------------------
		public function getColor(clr:uint):int
		{
			if ( clr == color )
				return iCount;
				
			else if ( clr < color ) {
				if ( leftBranch )
					return leftBranch.getColor(clr);
				
				return 0;
			}
			
			else if (clr > color ) {
				if ( rightBranch )
					return rightBranch.getColor(clr);
					
				return 0;
			}
			return 0;
		}
		
		// --------------------------------------------------------------------
		// get the next entry, use after calling getFirst()
		// *** Need to use pointer for count... *** will not work as is.
		// --------------------------------------------------------------------
		public function getAscend(index:int,	// [in] index to seek
								  count:int)	// [out] current index in tree
								  :uint			// [out] color at index seeked
		{
			var clr:uint;
			
			if ( leftBranch ) {
				clr = leftBranch.getAscend(index, count);
				if ( clr >= 0 )
					return clr;
			}
			
			if ( index == count ) 
				return color;
				
			count ++;
			
			if ( rightBranch )	
				return rightBranch.getAscend(index, count);
				
			return 0;
		}
		
		// --------------------------------------------------------------------
		// returns all entries sorted in an array
		// --------------------------------------------------------------------
		public function getAll():Array
		{
			var list:Array;
			var list2:Array;
			
			if ( leftBranch )
				list = leftBranch.getAll();
				
			if ( rightBranch )
				list2 = rightBranch.getAll();
				
			var entry:Array = new Array();
			entry.push (this.color);
			entry.push (this.iCount);
			
			if ( !list ) 
				list = new Array();
				
			list.push(entry);
			if ( list2 )
				list = list.concat(list2);
			return list;
		}
		
///////////////////////////////////////////////////////////////////////////////
// Properties
		
		// --------------------------------------------------------------------
		// returns number of entries
		// --------------------------------------------------------------------
		public function get count():int
		{
			var num:int=0;
			
			if ( leftBranch )
				num = leftBranch.count;
				
			if ( rightBranch )
				num += rightBranch.count;
				
			return num ++;
		}
	}
}