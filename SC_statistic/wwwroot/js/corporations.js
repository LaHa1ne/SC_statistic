function ChangeCorpFilter() {
    let selectedFilterValue = $("#name-or-tag-filter-select").val();
    if (selectedFilterValue === 'tag') {
        $('#corporation_name_or_tag').attr('placeholder', 'Тег');
    } else if (selectedFilterValue === 'name') {
        $('#corporation_name_or_tag').attr('placeholder', 'Название');
    }
};

function FindCorporation() {
    let selectedFilterValue = $("#name-or-tag-filter-select").val();
    var name_or_tag = $('#corporation_name_or_tag').val();
    if (name_or_tag === '') {
        $(".corporation-notification").text(selectedFilterValue === 'tag' ? "Введите тег" : "Введите название");
        $(".corporation-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".corporation-notification").css("visibility", "hidden");
        }, 2000);
    }
    else {
        $.ajax({
            url: '/Corporations/GetCorporationInfo',
            data: selectedFilterValue === 'tag' ? { tag: name_or_tag } : { name: name_or_tag },
            type: "GET",
            dataType: "json",
            beforeSend: function () {
                $('.corporation-info-container').css('visibility', 'hidden');
                $('#player-table tr:not(:first-child)').remove();
                $(".corporation-notification").css("visibility", 'hidden');
            },
            success: function (response) {
                AddCorporationInfoFromJson(response.data);
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status == 404) $(".corporation-notification").text("Корпорация не найдена");
                else $(".corporation-notification").text("Внутренняя ошибка сервера");
                $(".corporation-notification").css("visibility", "visible");
            }
        });
    }
}

function AddCorporationInfoFromJson(data) {
    $('.corp-main-info h2').text(data.currentName + ' [' + data.currentTag + ']');
    $('.corp-main-info .pve-rating').text(data.pveRating);
    $('.corp-main-info .pvp-rating').text(data.pvpRating);

    let player_table = $('#player-table tbody');
    let empty_player = player_table.find('tr[name="empty_player"]');
    for (var player of data.players) {
        let new_corp_player = $(empty_player).clone(true);
        $(new_corp_player).attr('name', '');
        $(new_corp_player).children('td[name="nickname"]').text(player.currentNickname);
        if (player.isInformationCorrect) {
            $(new_corp_player).children('td[name="status"]').text('Актуальная информация');
            $(new_corp_player).children('td[name="status"]').addClass('status-actual');
        }
        else {
            $(new_corp_player).children('td[name="status"]').text('Требуется обновление');
            $(new_corp_player).children('td[name="status"]').addClass('status-update');
        }
        $(new_corp_player).css('display', 'table-row');
        $(player_table).append(new_corp_player);
    }
    $('.corporation-info-container').css('visibility', 'visible');
}

