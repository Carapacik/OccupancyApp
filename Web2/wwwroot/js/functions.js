function post(){
}

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