function ChangeNotificationType() {
    var selectedType = $('#notification-filter-select').val();
    var jsonString = JSON.stringify({ notificationId: null, type: selectedType == "all" ? null : selectedType });
    $.ajax({
        url: '/Notifications/GetNotificationsWithSelectedType',
        data: jsonString,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            if (response.statusCode == 200) {
                var notification_list = $('.notification-list');
                notification_list.find('li:not(:last-child)').remove();
                AddNotificationsFronJsonList(response.data.notifications, "end");
                if (response.data.hasMoreNotifications) $("#load_more_notifications").css('display', 'inline-block');
                else $("#load_more_notifications").css('display', 'none');
            }
        },
        error: function (jqXHR, exception) {
            alert("Ошибка");
        }
    });
}

function LoadMoreNotifications() {
    var notificationId = $('.notification-list li:first').attr('name');
    var selectedType = $('#notification-filter-select').val();
    var jsonString = JSON.stringify({ notificationId: notificationId, type: selectedType == "all" ? null : selectedType });
    $.ajax({
        url: '/Notifications/GetMoreNotificationsWithSelectedType',
        data: jsonString,
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            if (response.statusCode == 200) {
                AddNotificationsFronJsonList(response.data.notifications, "begin");
                if (response.data.hasMoreNotifications) $("#load_more_notifications").css('display', 'inline-block');
                else $("#load_more_notifications").css('display', 'none');
            }
        },
        error: function (jqXHR, exception) {
            alert("Ошибка");
        }
    });
}

function AddNotificationsFronJsonList(notificaion_list_json, position_in_the_list) {
    let notification_list = $('.notification-list');
    let empty_notification = notification_list.find('li[name="empty_notification"]');
    for (var notification of notificaion_list_json) {
        let new_notification = $(empty_notification).clone(true);
        $(new_notification).attr('name', notification.notificationId);
        $(new_notification).children('div.list-text').text(notification.text);
        $(new_notification).children('div.date').text(notification.date);
        switch (notification.type) {
            case "PlayerChangedNickname":
                $(new_notification).addClass('red');
                break;
            case "PlayerChangedCorporation":
                $(new_notification).addClass('blue');
                break;
            case "CorporationChangedName":
                $(new_notification).addClass('green');
                break;
            default:
                $(new_notification).addClass('yellow');
                break;
        }

        if (position_in_the_list == "end") $(empty_notification).before(new_notification)
        else $(notification_list).prepend(new_notification);

    }
}