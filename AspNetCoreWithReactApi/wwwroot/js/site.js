// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    //$('#columnsToImportTable td.tick').click(function (e) {
    //    e.stopPropagation();
    //    e.preventDefault();
    //    var $this = $(this);
    //    // get the ImportColumn hidden field.
    //    var hiddentag = $(this).find("input[type='hidden']").clone();
    //    if ($this.hasClass('true')) {
    //        //change the hidend field' value
    //        $(hiddentag).attr("value", "false");
    //        //get the html
    //        var newtag = $(hiddentag)[0].outerHTML;
    //        $this.html('<td id="importColumn" class="greyOutBackground centreText pointerCursor red tick false centreElement">&#10006;' + newtag + '</td>');
    //    } else {
    //        $(hiddentag).attr("value", "true");
    //        var newtag = $(hiddentag)[0].outerHTML;
    //        $this.html('<td id="importColumn" class="greyOutBackground centreText pointerCursor green tick true centreElement">&#10004;' + newtag + '</td>');
    //    }
    //    $this.toggleClass('true');
    //});

    $(".editdiv").each(function (index, item) {
        $(item).on('propertychange input', function (e) {
            $(this).next("input[type='hidden']").val($(this).text());
        });
    });
});

