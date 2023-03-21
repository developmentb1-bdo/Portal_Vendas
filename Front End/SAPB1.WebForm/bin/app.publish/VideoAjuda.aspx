<%@ Page 
    Title="" 
    Language="C#" 
    MasterPageFile="~/SapB1Master.Master" 
    AutoEventWireup="true" 
    CodeBehind="VideoAjuda.aspx.cs" 
    Inherits="SAPB1.WebForm.VideoAjuda" 
%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField
        runat="server"
        ID="hfVideo" />
    <div class="box box-warning">
        <div class="box-header with-border">
            <h3 class="box-title">
                Filtros de Pesquisa
            </h3>
            <div class="box-tools pull-right">
               <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
           </div>
        </div>
        <div class="box-body" style="display: block;">
            <div class="embed-responsive embed-responsive-16by9">
                <div class="col-md-12" id="player">
                    
                </div>
           </div>
        </div>
     </div>

    <script>
      // 2. This code loads the IFrame Player API code asynchronously.
      var tag = document.createElement('script');

      tag.src = "https://www.youtube.com/iframe_api";
      var firstScriptTag = document.getElementsByTagName('script')[0];
      firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

      // 3. This function creates an <iframe> (and YouTube player)
      //    after the API code downloads.
      var player;
      function onYouTubeIframeAPIReady() {

          var valor = document.getElementById('<%= hfVideo.ClientID %>');

          player = new YT.Player('player', {
              height: '360',
              width: '640',
              videoId: valor.value,
              events: {
                  'onReady': onPlayerReady,
                  'onStateChange': onPlayerStateChange
              }
          });
      }

      // 4. The API will call this function when the video player is ready.
      function onPlayerReady(event) {
        event.target.playVideo();
      }

      // 5. The API calls this function when the player's state changes.
      //    The function indicates that when playing a video (state=1),
      //    the player should play for six seconds and then stop.
      var done = false;
      function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING && !done) {
          setTimeout(stopVideo, 6000);
          done = true;
        }
      }
      function stopVideo() {
        player.stopVideo();
      }
    </script>
</asp:Content>
