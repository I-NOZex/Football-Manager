// THEME OPTIONS.JS
//--------------------------------------------------------------------------------------------------------------------------------
//This is JS file that contains skin, layout Style and bg used in this template*/
// -------------------------------------------------------------------------------------------------------------------------------
// Template Name: Sports Cup- Responsive HTML5  soccer and sports Template.
// Author: Iwthemes.
// Name File: theme-options.js
// Version 1.0 - Created on 20 May 2014
// Website: http://www.iwthemes.com 
// Email: support@iwthemes.com
// Copyright: (C) 2014
// -------------------------------------------------------------------------------------------------------------------------------

  $(document).ready(function($) {

  	/* Selec your skin and layout Style */
/*
	function interface(){

    // Skin value
    var skin = "green"; // green (default), red ,yellow,purple,blue, orange, purple, pink, cocoa, custom 

    // Boxed value
    var layout = "layout-semiboxed"; // layout-semiboxed(default), layout-boxed, layout-boxed-margin ,layout-wide

    //Only in boxed version 
    var bg = "none";  // none (default), bg1, bg2, bg3, bg4, bg5, bg6, bg7, bg8, bg9, bg10, bg11 

    // Theme Panel - disable panel options
    var themepanel = "1"; // 1 (default - enable), 0 ( disable)

    $(".skin").attr("href", "css//" + skin + ".css");
    $("#layout").addClass(layout);	
    $("body").addClass(bg);   
    $("#theme-options").css('opacity' , themepanel);
    return false;
  }

 	interface();
*/


	//=================================== Theme Options ====================================//

	$('.wide').click(function() {
		$('.boxed').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'none');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-wide');
	});
	$('.semiboxed').click(function() {
		$('.wide').removeClass('active');
		$('.boxed').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-wide').removeClass('layout-boxed').removeClass('layout-boxed-margin').addClass('layout-semiboxed');
	});
	$('.boxed').click(function() {
		$('.wide').removeClass('active');
		$('.boxed-margin').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-boxed-margin').removeClass('layout-wide').addClass('layout-boxed');
	});
	$('.boxed-margin').click(function() {
		$('.boxed').removeClass('active');
		$('.wide').removeClass('active');
		$('.semiboxed').removeClass('active');
		$(this).addClass('active');
		$('.patterns').css('display' , 'block');
		$('#layout').removeClass('layout-semiboxed').removeClass('layout-wide').removeClass('layout-boxed').addClass('layout-boxed-margin');
	});

	//=================================== Skins Changer ====================================//
/*
	google.setOnLoadCallback(function(){

	'use strict';

    // Color changer
    $(".red").click(function(){
	   	$(".skin").attr("href", "css/skins/red/red.css");
	    return false;
	});
	$(".blue").click(function(){
	    $(".skin").attr("href", "css/skins/blue/blue.css");
	    return false;
	});
	$(".yellow").click(function(){
	    $(".skin").attr("href", "css/skins/yellow/yellow.css");
	    return false;
	});
	$(".green").click(function(){
	    $(".skin").attr("href", "css/skins/green/green.css");
	    return false;
	});
	$(".orange").click(function(){
    	$(".skin").attr("href", "css/skins/orange/orange.css");
    	return false;
	});
	$(".purple").click(function(){
	    $(".skin").attr("href", "css/skins/purple/purple.css");
	    return false;
	});
	$(".pink").click(function(){
	    $(".skin").attr("href", "css/skins/pink/pink.css");
	    return false;
	});
	$(".cocoa").click(function(){
        $(".skin").attr("href", "css/skins/cocoa/cocoa.css");
        return false;
   	});
 });
*/
	//=================================== Background Options ====================================//
	
	$('#theme-options ul.backgrounds li').click(function(){
	var 	$bgSrc = $(this).css('background-image');
		if ($(this).attr('class') == 'bgnone')
			$bgSrc = "none";

		$('body').css('background-image',$bgSrc);
		$.cookie('background', $bgSrc);
		$.cookie('backgroundclass', $(this).attr('class').replace(' active',''));
		$(this).addClass('active').siblings().removeClass('active');
	});

	//=================================== Panel Options ====================================//

	$('.openclose').click(function(){
		if ($('#theme-options').css('left') == "-220px")
		{
			$left = "0px";
			$.cookie('displayoptions', "0");
		} else {
			$left = "-220px";
			$.cookie('displayoptions', "1");
		}
		$('#theme-options').animate({
			left: $left
		},{
			duration: 500			
		});

	});

	$(function(){
		$('#theme-options').fadeIn();
		$bgSrc = $.cookie('background');
		$('body').css('background-image',$bgSrc);

		if ($.cookie('displayoptions') == "1")
		{
			$('#theme-options').css('left','-220px');
		} else if ($.cookie('displayoptions') == "0") {
			$('#theme-options').css('left','0');
		} else {
			$('#theme-options').delay(800).animate({
				left: "-220px"
			},{
				duration: 500				
			});
			$.cookie('displayoptions', "1");
		}
		$('#theme-options ul.backgrounds').find('li.' + $.cookie('backgroundclass')).addClass('active');

	});

});