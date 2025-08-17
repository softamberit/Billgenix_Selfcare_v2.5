// JScript File

    $(document).ready(function(){
        $(".show").hide();
        $(".hide").click(function(){
        $(".hide").hide();
        $(".show").show();
        $(".mpanel").hide();        
        $(".cph").css("width","978px");        
  });
        $(".show").click(function(){
        $(".hide").show();
        $(".show").hide();
        $(".mpanel").show();        
        $(".cph").css("width","749px");        
  });
   });