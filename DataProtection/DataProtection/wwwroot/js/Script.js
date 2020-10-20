function scrollToElementId(hash) {
    const el = document.getElementById(hash);
    el.scrollIntoView({
        behavior: 'smooth',
        block: 'end',
        alignToTop: false
    });

}

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}