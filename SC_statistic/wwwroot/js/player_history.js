function Find_player_history(){
    var nickname = $('#player_nickname').val();

    $('.history-container').css('visibility', 'hidden');
    $('#nickname-list li:not(:first-child)').remove();
    $('#corporation-list li:not(:first-child)').remove();
    $(".player-notification").css("visibility", 'hidden');

    if (nickname === '') {
        $(".player-notification").text("Введите никнейм");
        $(".player-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".player-notification").css("visibility", "hidden");
        }, 2000);
    }
    else if (nickname.length < 2 || nickname.length > 16) {
        $(".player-notification").text("Длина никнейма от 2 до 16 символов");
        $(".player-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".player-notification").css("visibility", "hidden");
        }, 2000);
    }
    else {
        $.ajax({
            url: '/PlayerHistory/GetPlayerHistory',
            data: {nickname: nickname},
            type: "GET",
            dataType: "json",
            success: function (response) {
                AddPlayerHistoryFromJson(response.data);
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status == 404) $(".player-notification").text("Игрок с таким никнеймом не найден");
                else $(".player-notification").text("Внутренняя ошибка сервера");
                $(".player-notification").css("visibility", "visible");
            }
        });
    }
}

function AddPlayerHistoryFromJson(data)
{
    let nickname_list = $('#nickname-list');
    let corporation_list = $('#corporation-list');
    let empty_nickname_history = nickname_list.find('li[name="empty_nickname"]');
    let empty_corporation_history = corporation_list.find('li[name="empty_corporation"]');
    for (var nickname_history of data.nicknameHistory.reverse()) {
        let new_nickname_history = $(empty_nickname_history).clone(true);
        $(new_nickname_history).attr('name', '');
        $(new_nickname_history).children('span[name="nickname"]').text(nickname_history.nickname);
        $(new_nickname_history).children('span[name="date"]').text(nickname_history.date);
        $(new_nickname_history).css('display', 'flex');
        $(nickname_list).append(new_nickname_history);
    }
    for (var corporation_history of data.corporationHistory.reverse()) {
        let new_corporation_history = $(empty_corporation_history).clone(true);
        $(new_corporation_history).attr('name', '');
        $(new_corporation_history).children('span[name="nameAndTag"]').text(corporation_history.nameAndTag);
        $(new_corporation_history).children('span[name="date"]').text(corporation_history.date);
        $(new_corporation_history).css('display', 'flex');
        $(corporation_list).append(new_corporation_history);
    }

    $('.history-container').css('visibility', 'visible');
    if (!data.isInformationCorrect) {
        $(".player-notification").text("Информация не актуальна");
        $(".player-notification").css("visibility", "visible");
    }
}