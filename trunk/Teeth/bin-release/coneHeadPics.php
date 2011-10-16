<html>
<body>
<p>

<?php
$path = "../../flex/coneHeadPics/assets/";

if ($handle = opendir($path)) {

    /* This is the correct way to loop over the directory. */
    $count = 0;
    while (false !== ($file = readdir($handle))) {
		if ( $count > 1 ) {
			printf("<data> <thumbnail><url>http://www.ctyeung.com/flex/coneHeadPics/assets/%s", $file);
			printf("</url></thumbnail>");
			printf("<credit>%s", $file);
			printf("</credit></data>"); 
		}
		$count ++;
    }
    closedir($handle);
}
?>
</p>
</body>
</html>