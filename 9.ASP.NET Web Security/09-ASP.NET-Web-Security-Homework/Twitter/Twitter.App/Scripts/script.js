$(function() {
    var twitterHub = $.connection.twitterHub;

    twitterHub.client.receiveTweet = function (tweetId) {

        alert("TweetId = " + tweetId);
        
        // Cannot get div with new tweet :(
        $.get("/Tweets/GetTweet/" + tweetId), function (result) {
            $("#new-tweet").prepend(result);
        }
    }

    twitterHub.client.receiveNotification = function () {
        alert("TODO");
    }

    $.connection.hub.start()
    .done(function () {
        console.log('Hub started');
    });
});