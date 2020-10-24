function ChangeColor(productamount, coloramount,id)
{
    var amount = parseInt(productamount) + parseInt(coloramount);
    var price = numberFormat(amount) + 'تومان';
    $("#price").html(price);
    $("[id*=color]").each(function () {
        var valueCheckId = $(this).attr("id");
        var color = $("#" + valueCheckId);
        color.removeClass('active-color');
    });
    var active = $("#color_" + id);
    active.addClass('active-color');
}
function numberFormat(number) {

    var digitCount = (number + "").length;
    var formatedNumber = number + "";
    var ind = digitCount % 3 || 3;
    var temparr = formatedNumber.split('');

    if (digitCount > 3 && digitCount <= 6) {

        temparr.splice(ind, 0, ',');
        formatedNumber = temparr.join('');

    } else if (digitCount >= 7 && digitCount <= 15) {
        var temparr2 = temparr.slice(0, ind);
        temparr2.push(',');
        temparr2.push(temparr[ind]);
        temparr2.push(temparr[ind + 1]);
        // temparr2.push( temparr[ind + 2] ); 
        if (digitCount >= 7 && digitCount <= 9) {
            temparr2.push(" million");
        } else if (digitCount >= 10 && digitCount <= 12) {
            temparr2.push(" billion");
        } else if (digitCount >= 13 && digitCount <= 15) {
            temparr2.push(" trillion");

        }
        formatedNumber = temparr2.join('');
    }
    return formatedNumber;
}