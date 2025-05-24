// Assets/Plugins/WebGL/BrowserUtils.jslib
mergeInto(LibraryManager.library, {
  Browser_GoBack: function() {
    window.history.back();
  },
  Browser_Reload: function() {
    window.location.reload();
  }
});
