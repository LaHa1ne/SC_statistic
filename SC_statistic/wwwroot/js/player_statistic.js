function Add_player_statistic(operation_type) {

    $('.player-stat-container').css('visibility', 'hidden');
    $('.choose-and-add-session-container').css('visibility', 'hidden');
    $('.session-list-and-add-checkpoint-container').css('visibility', 'hidden');
    $('.notification').css('visibility', 'hidden');

    $('.session-select option:not([name="empty_option"])').remove();

    var nickname = '';
    if (operation_type === 'select') {
        nickname = $('.player-select').val();
        if (nickname === '') return;
    }
    else if (operation_type === 'add') nickname = $('#nickname_of_added_player').val();

    if (nickname === '') {
        let emptyPlayerOption = $('.player-select option[name="empty_option"]');
        emptyPlayerOption.prop('selected', true);

        $(".player-notification").text("Введите никнейм");
        $(".player-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".player-notification").css("visibility", "hidden");
        }, 2000);
    }
    else if (nickname.length < 2 || nickname.length > 16) {
        let emptyPlayerOption = $('.player-select option[name="empty_option"]');
        emptyPlayerOption.prop('selected', true);

        $(".player-notification").text("Длина никнейма от 2 до 16 символов");
        $(".player-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".player-notification").css("visibility", "hidden");
        }, 2000);
    }
    else {
        $.ajax({
            url: '/PlayerStatistic/GetTrackedPlayer',
            data: { nickname: nickname },
            type: "GET",
            dataType: "json",
            success: function (response) {
                AddTrackedPlayerStatisticFromJson(response.data);
                $('#nickname_of_added_player').val('');
                $('.player-stat-container').css('visibility', 'visible');
                $('.choose-and-add-session-container').css('visibility', 'visible');
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status == 404) $(".player-notification").text("Игрок с таким никнеймом не найден");
                else if (jqXHR.status == 422) $(".player-notification").text("Введен устаревший никнейм");
                else $(".player-notification").text("Внутренняя ошибка сервера");
                $(".player-notification").css("visibility", "visible");
            }
        });
    }
}

function AddTrackedPlayerStatisticFromJson(data) {
    let playerOption = $('.player-select option[value="' + data.stat.nickname + '"]');
    if (playerOption.length === 0) {
        playerOption = $('.player-select option[name="empty_option"]').clone();
        playerOption.text(data.stat.nickname);
        playerOption.attr('value', data.stat.nickname);
        playerOption.attr('name', data.trackedPlayerId);
        $('.player-select').append(playerOption);
    }
    playerOption.prop('selected', true);

    let player_stat_container = $('.player-stat-container');
    $(player_stat_container).find('div[name="nickname"]').text(data.stat.nickname);
    $(player_stat_container).find('div[name="corporation"]').text(data.stat.corporation);
    $(player_stat_container).find('span[name="accountRank"]').text(data.stat.accountRank);
    $(player_stat_container).find('span[name="prestigeBonus"]').text(data.stat.prestigeBonus);
    $(player_stat_container).find('span[name="karma"]').text(data.stat.karma);

    let player_stat_scroll_container = $('.player-statistic-scroll');
    const stat_group_names = ['pvpStat','pvpEff','pveStat','coopStat','unlimPveMissionLevels'];
    for (let stat_group_name of stat_group_names) {
        for (let stat_name in data.stat[stat_group_name]) {
            let stat_element = $(player_stat_scroll_container).find('div.' + stat_group_name + ' span[name="' + stat_name + '"]');
            $(stat_element).text(data.stat[stat_group_name][stat_name]);
        }
    }

    let sessionOptionmaxLength = 55;
    for (let session of data.sessions) {
        let newSessionOption = $('.session-select option[name="empty_option"]').clone();
        let newSessionOptionText = `${session.name}${'\u00A0'.repeat(sessionOptionmaxLength - (session.name.length + session.startDate.length))}${session.startDate}`;
        newSessionOption.text(newSessionOptionText);
        newSessionOption.attr('value', session.sessionId);
        newSessionOption.attr('name', session.name);
        $('.session-select').append(newSessionOption);
    }
}

function Add_session_statistic(operation_type) {

    $('.session-list-and-add-checkpoint-container').css('visibility', 'hidden');
    $('.notification').css('visibility', 'hidden');

    $('.checkpoint-list-scrollable-container').children('.checkpoint-item:not([name="empty_checkpoint"])').remove();

    let sessionId = operation_type === 'select' ? $('.session-select').val() : null;
    let sessionName = operation_type === 'select' ? "" : $('#name_of_added_session').val();
    let trackedPlayerId = $('.player-select option:selected').attr('name');
    let trackedPlayerNickname = $('.player-select').val();

    if (operation_type === 'select') {
        if (sessionId === '') return;
    }

    if (operation_type === 'add') {
        if (sessionName === '') {
            $(".session-notification").text("Введите название");
            $(".session-notification").css("visibility", "visible");
            setTimeout(function () {
                $(".session-notification").css("visibility", "hidden");
            }, 2000);
            return;
        }
        if (sessionName.length > 16) {
            $(".session-notification").text("Длина названия до 16 символов");
            $(".session-notification").css("visibility", "visible");
            setTimeout(function () {
                $(".session-notification").css("visibility", "hidden");
            }, 2000);
            return;
        }
    }

    var jsonstring = JSON.stringify({ sessionId: sessionId, sessionName: sessionName, trackedPlayerId: trackedPlayerId, trackedPlayerNickname: trackedPlayerNickname });
    $.ajax({
        url: '/PlayerStatistic/GetSession',
        data: jsonstring,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            AddSessionStatisticFromJson(response.data, operation_type)
            $('.session-list-and-add-checkpoint-container').css('visibility', 'visible');
            $('#name_of_added_session').val('');
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status == 404) $(".session-notification").text("Никнейм угрока устарел");
            else if (jqXHR.status == 422) $(".session-notification").text("Никнейм угрока устарел");
            else $(".session-notification").text("Внутренняя ошибка сервера");
            $(".session-notification").css("visibility", "visible");
        }
    });
}

function AddSessionStatisticFromJson(data, operation_type) {
    let sessionOptionmaxLength = 55;
    let sessionOption = $('.session-select option[value="' + data.sessionId + '"]');
    if (operation_type === 'add') {
        sessionOption = $('.session-select option[name="empty_option"]').clone();
        sessionOptionText = `${data.name}${'\u00A0'.repeat(sessionOptionmaxLength - (data.name.length + data.startDate.length))}${data.startDate}`;
        sessionOption.text(sessionOptionText);
        sessionOption.attr('value', data.sessionId);
        sessionOption.attr('name', data.name);
        $('.session-select').append(sessionOption);
    }
    sessionOption.prop('selected', true);

    let session_start_container = $('.session-start-item');
    $(session_start_container).attr('name', data.startCheckpoint.checkpointId);
    $(session_start_container).find('div.date').text(data.startCheckpoint.date);
    $(session_start_container).find('div.time').text(data.startCheckpoint.time);
    for (let stat_name in data.startCheckpoint.checkpointStat) {
        let stat_element = $(session_start_container).find('.checkpoint-body span[name="' + stat_name + '"]');
        $(stat_element).text(data.startCheckpoint.checkpointStat[stat_name]);
    }

    let checkpoint_list_container = $('.checkpoint-list-scrollable-container');
    for (let checkpoint of data.checkpoints) {
        let newCheckpoint = $(checkpoint_list_container).find('.checkpoint-item[name="empty_checkpoint"]').clone();
        $(newCheckpoint).attr('name', data.startCheckpoint.checkpointId);
        $(newCheckpoint).find('div.header-text').text(checkpoint.name);
        $(newCheckpoint).find('div.date').text(checkpoint.date);
        $(newCheckpoint).find('div.time').text(checkpoint.time);
        for (let stat_name in checkpoint.checkpointStat) {
            let stat_element = $(newCheckpoint).find('.checkpoint-body span[name="' + stat_name + '"]');
            $(stat_element).text(checkpoint.checkpointStat[stat_name]);
        }
        $(newCheckpoint).css("display", "block");
        $(checkpoint_list_container).append(newCheckpoint);
    }

    let session_average_values_container = $('.average-values-item');
    $(session_average_values_container).attr('name', data.averageValues.checkpointId);
    $(session_average_values_container).find('div.date').text(data.averageValues.date);
    $(session_average_values_container).find('div.time').text(data.averageValues.time);
    for (let stat_name in data.averageValues.checkpointStat) {
        let stat_element = $(session_average_values_container).find('.checkpoint-body span[name="' + stat_name + '"]');
        $(stat_element).text(data.averageValues.checkpointStat[stat_name]);
    }
}

function Add_checkpoint() {

    $('.notification').css('visibility', 'hidden');

    var checkpoint_name = $('#name_of_added_checkpoint').val();

    if (checkpoint_name === '') {
        $(".checkpoint-notification").text("Введите название");
        $(".checkpoint-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".checkpoint-notification").css("visibility", "hidden");
        }, 2000);
        return;
    }

    if (checkpoint_name.length > 10) {
        $(".checkpoint-notification").text("Длина названия до 10 символов");
        $(".checkpoint-notification").css("visibility", "visible");
        setTimeout(function () {
            $(".checkpoint-notification").css("visibility", "hidden");
        }, 2000);
        return;
    }

    let sessionId = $('.session-select').val();
    let trackedPlayerId = $('.player-select option:selected').attr('name');

    var jsonstring = JSON.stringify({ sessionId: sessionId, trackedPlayerId: trackedPlayerId, checkpointName: checkpoint_name });
    $.ajax({
        url: '/PlayerStatistic/AddCheckpoint',
        data: jsonstring,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            AddCheckpointFromJson(response.data);
            $('#name_of_added_checkpoint').val('');
        },
        error: function (jqXHR, exception) {
            if (jqXHR.status == 404) $(".checkpoint-notification").text("Никнейм угрока устарел");
            else if (jqXHR.status == 422) $(".checkpoint-notification").text("С момента последнего чекпоинта сыграно 0 боев");
            else $(".checkpoint-notification").text("Внутренняя ошибка сервера");
            $(".checkpoint-notification").css("visibility", "visible");
        }
    });
}

function AddCheckpointFromJson(data){
    let checkpoint_list_container = $('.checkpoint-list-scrollable-container');
    let newCheckpoint = $(checkpoint_list_container).find('.checkpoint-item[name="empty_checkpoint"]').clone();
    $(newCheckpoint).attr('name', data.newCheckpoint.checkpointId);
    $(newCheckpoint).find('div.header-text').text(data.newCheckpoint.name);
    $(newCheckpoint).find('div.date').text(data.newCheckpoint.date);
    $(newCheckpoint).find('div.time').text(data.newCheckpoint.time);
    for (let stat_name in data.newCheckpoint.checkpointStat) {
        let stat_element = $(newCheckpoint).find('.checkpoint-body span[name="' + stat_name + '"]');
        $(stat_element).text(data.newCheckpoint.checkpointStat[stat_name]);
    }
    $(newCheckpoint).css("display", "block");
    $(checkpoint_list_container).append(newCheckpoint);

    let session_average_values_container = $('.average-values-item');
    $(session_average_values_container).attr('name', data.averageValues.checkpointId);
    $(session_average_values_container).find('div.date').text(data.averageValues.date);
    $(session_average_values_container).find('div.time').text(data.averageValues.time);
    for (let stat_name in data.averageValues.checkpointStat) {
        let stat_element = $(session_average_values_container).find('.checkpoint-body span[name="' + stat_name + '"]');
        $(stat_element).text(data.averageValues.checkpointStat[stat_name]);
    }
}