function post(){
}

$('form').submit(function () {

    var obj = $('form').serializeJSON();

    $.ajax({
        type: 'POST',
        url: 'http://localhost:53760/api/roomtype-settings',
        dataType: 'json',
        data: JSON.stringify(obj),
        contentType: 'application/json',
        success: function (data) {
            alert(data)
        }
    });

var index = 0;

function add() {
    $('br:last').after('<input name="occupancyFrom" class="input_item" required />');
    $('input:last').after('<input name="occupancyTo" class="input_item" required />');
    $('input:last').after('<input name="rateLevel" class="input_item ratelevel" required />');
    $('input:last').after('<br/>');
    index++;
}

function remove() {
    if (index > 0) {
        $('br:last').remove();
        $('input:last').remove();
        $('input:last').remove();
        $('input:last').remove();
        index--;
    }
}