
$(function () {
    // Reference the auto-generated proxy for the hub.  
    var game = $.connection.gameHub;
    // Create a function that the hub can call back to display messages.
    game.client.handleResponse = function (sessionId, response) {
        $('#discussion').append('<li><strong>' + htmlEncode(sessionId)
            + '</strong>: ' + htmlEncode(response) + '</li>');
    };


    game.client.newChallenge = function (challenge) {

        $('#challenge').html(challenge);
    };

    game.client.isGameFull = function (value) {
        //do stuff is game is full
    }


    game.client.startGame = function () {
        //do stuff is game is full
        //TODO
    }

    game.client.loadCategories = function (categories) {
        $('#challenge').html(categories);
    }

    game.client.getCurrentCategory = function (currentCategory) {
        $('#challenge').html(currentCategory);
    }


    //
    game.client.getUserResponse = function (sessionId, response) {
        $('#myResponse').html(response);
    }

    //
    game.client.getWinner = function (sessionId) {
        $('#challenge').html("The Winner is " + sessionId);
    }

    game.client.setUserStatus = function (status) {
        user.canPlay = status;
        console.log(user.canPlay);
    }

    game.client.setCurrentPicker = function (name, sessionId) {

        console.log('session: ' + sessionId);
    }


    //$('#message').focus();
    // Start the connection.


    //    var sessionId = $('#sessionId').val();
    //    //var name = 'fake name';
    //    game.server.addUserToGame(sessionId,name);
    //    $('#sendmessage').click(function () {                    
    //        game.server.sendUserResponse(sessionId, $('#message').val());
    //        $('#message').val('').focus();
    //    });

    //    $('#newChallenge').click(function () {
    //        game.server.sendChallenge();
    //    });

    //    $('#startGame').click(function () {
    //        game.server.startGame();
    //    });

    //    $('#getCategories').click(function () {
    //        game.server.getCategories();
    //    });


    //    $('#getCurrentCategory').click(function () {
    //        game.server.getCurrentCategory();
    //    });

    //    $('#setCategory').click(function () {
    //        game.server.setCategory($('#category').val());
    //    });

    //    $('#sendResponse').click(function () {
    //        game.server.sendResponse($('#sessionId').val(), $('#response').val());
    //    });

    //});
});
// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}