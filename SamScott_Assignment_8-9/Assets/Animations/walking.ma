//Maya ASCII 2017 scene
//Name: walking.ma
//Last modified: Fri, Nov 03, 2017 05:19:18 PM
//Codeset: 1252
requires maya "2017";
requires "stereoCamera" "10.0";
requires "stereoCamera" "10.0";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2017";
fileInfo "version" "2017";
fileInfo "cutIdentifier" "201606150345-997974";
fileInfo "osv" "Microsoft Windows 8 , 64-bit  (Build 9200)\n";
fileInfo "license" "education";
createNode clipLibrary -n "clipLibrary1";
	rename -uid "5B3D4415-4D1C-4AE5-8AA7-089DD1A3999C";
	setAttr -s 6 ".cel[0].cev";
	setAttr ".cd[0].cm" -type "characterMapping" 6 "root_joint.rotateZ" 2 
		1 "root_joint.rotateY" 2 2 "root_joint.rotateX" 2 3 "root_joint.translateZ" 
		1 1 "root_joint.translateY" 1 2 "root_joint.translateX" 1 
		3  ;
	setAttr ".cd[0].cim" -type "Int32Array" 6 0 1 2 3 4
		 5 ;
createNode animClip -n "walkingSource";
	rename -uid "FE792B45-40C6-5C3E-D2CC-C18862F9A047";
	setAttr ".ihi" 0;
	setAttr -s 6 ".ac[0:5]" yes yes yes yes yes yes;
	setAttr ".ss" 1;
	setAttr ".se" 501;
	setAttr ".ci" no;
createNode animCurveTA -n "root_joint_rotateZ";
	rename -uid "8C42DBD3-4482-FE4A-2C6F-89988A1DA11E";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 90;
createNode animCurveTA -n "root_joint_rotateY";
	rename -uid "950441E5-4BFF-A88B-5338-D1ABFA2EC63D";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 7.7156545157716039;
createNode animCurveTA -n "root_joint_rotateX";
	rename -uid "D48EEC77-41A1-08A4-EE40-EBBC238094F1";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 90;
createNode animCurveTL -n "root_joint_translateZ";
	rename -uid "940298DA-4E97-D381-8EDB-868171D7FBB5";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 -0.87970298528671265;
createNode animCurveTL -n "root_joint_translateY";
	rename -uid "F79CD847-46AE-0EBE-F970-71BA220FD264";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 20.116756439208984;
createNode animCurveTL -n "root_joint_translateX";
	rename -uid "460CBBBD-4EB3-78AA-048B-BEA0AC8467BB";
	setAttr ".tan" 18;
	setAttr ".ktv[0]"  0 0;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 9 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 11 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :characterPartition;
select -ne :ikSystem;
	setAttr -s 4 ".sol";
connectAttr "walkingSource.cl" "clipLibrary1.sc[0]";
connectAttr "root_joint_rotateZ.a" "clipLibrary1.cel[0].cev[0].cevr";
connectAttr "root_joint_rotateY.a" "clipLibrary1.cel[0].cev[1].cevr";
connectAttr "root_joint_rotateX.a" "clipLibrary1.cel[0].cev[2].cevr";
connectAttr "root_joint_translateZ.a" "clipLibrary1.cel[0].cev[3].cevr";
connectAttr "root_joint_translateY.a" "clipLibrary1.cel[0].cev[4].cevr";
connectAttr "root_joint_translateX.a" "clipLibrary1.cel[0].cev[5].cevr";
// End of walking.ma
