var FileBrowserWebgl = {
    // Open file.
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filter: Filter files. Example filters:
    //     Match all image files: "image/*"
    //     Match all video files: "video/*"
    //     Match all audio files: "audio/*"
    //     Custom: ".plist, .xml, .yaml"
    // multiselect: Allows multiple file selection

    UploadFile: function(gameObjectNamePtr, methodNamePtr, filterPtr)
    {
        gameObjectName = Pointer_stringify(gameObjectNamePtr);
        methodName = Pointer_stringify(methodNamePtr);
        filter = Pointer_stringify(filterPtr);

        // Delete if element exist
        var fileInput = document.getElementById(gameObjectName)
        if (fileInput)
        {
            document.body.removeChild(fileInput);
        }

        fileInput = document.createElement('input');
        fileInput.setAttribute('id', gameObjectName);
        fileInput.setAttribute('type', 'file');
        fileInput.setAttribute('style','display:none;');
        fileInput.setAttribute('style','visibility:hidden;');
        fileInput.setAttribute('accept', filter);

        fileInput.onchange = function (event)
        {
            // multiselect works
            if(event.target.files.length > 0)
            {
                var obj = {
                    name: event.target.files[0].name,
                    url: URL.createObjectURL(event.target.files[0])
                };
                // File selected
                SendMessage(gameObjectName, methodName, JSON.stringify(obj));
            }

            // Remove after file selected
            document.body.removeChild(fileInput);
        }

        document.onmouseup = function() {
            fileInput.click();
            document.onmouseup = null;
            console.log("onmouseup");
        }

        document.body.appendChild(fileInput);
    },

    // Save file
    // DownloadFile method does not open SaveFileDialog like standalone builds, its just allows user to download file
    // gameObjectNamePtr: Unique GameObject name. Required for calling back unity with SendMessage.
    // methodNamePtr: Callback method name on given GameObject.
    // filenamePtr: Filename with extension
    // byteArray: byte[]
    // byteArraySize: byte[].Length
    DownloadFile: function(gameObjectNamePtr, methodNamePtr, filenamePtr, byteArray, byteArraySize) {
        gameObjectName = Pointer_stringify(gameObjectNamePtr);
        methodName = Pointer_stringify(methodNamePtr);
        filename = Pointer_stringify(filenamePtr);

        var bytes = new Uint8Array(byteArraySize);
        for (var i = 0; i < byteArraySize; i++) {
            bytes[i] = HEAPU8[byteArray + i];
        }

        var downloader = window.document.createElement('a');
        downloader.setAttribute('id', gameObjectName);
        downloader.href = window.URL.createObjectURL(new Blob([bytes], { type: 'application/octet-stream' }));
        downloader.download = filename;
        document.body.appendChild(downloader);

        document.onmouseup = function()
        {
            downloader.click();
            document.body.removeChild(downloader);
        	document.onmouseup = null;

            SendMessage(gameObjectName, methodName);
        }
    },

    ShowIFrame : function(iframeId)
    {
        var frameId = Pointer_stringify(iframeId);
        console.log("!!!!!!!!!!!!!!! " + frameId);
        var iframeObj = document.getElementById(frameId);
        iframeObj.setAttribute('style','display:block;');
    },

    HideIFrame : function(iframeId)
    {
        var frameId = Pointer_stringify(iframeId);
        console.log("@@@@@@@@@@@@ " + frameId);
        var iframeObj = document.getElementById(frameId);
        iframeObj.setAttribute('style','display:none;');
    },
};

mergeInto(LibraryManager.library, FileBrowserWebgl);