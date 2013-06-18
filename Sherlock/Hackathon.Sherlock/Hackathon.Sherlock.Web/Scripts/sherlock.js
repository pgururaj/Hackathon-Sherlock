/*

Layout
	Bootstrap
	basic
	better
	polished

Functionality

Login
	Player
	Viewer

Alex:
	# Intro (login or view)
	# Start game players = 4


	# Determine Player with turn (1st player to join)
	# Present Selection
		# Present question board
		# [UI] Accept Player Choice
		# [UI] Disable Choice

	# [UI]Present Challenge
	# Wait for Response
		# Sends buzzer
		# Evaluate Player Response
			# Check degree of confidence agains Challenge and Prepared Response
				# if Reject -> Wait for response
				# if Rejected all the go to next question
				# if Accept 
					# award $$$
					# give Player choice
	# Present Selection


Sherlock:
	# Consume challenge
	# Buzz in response
	# Make selection from screen


Player
	# Login
	# Enter Response
	# Make selection from screen

*/

var game = $.connection.gameHub;

$.connection.hub.start().done(function () {
    var sessionId = $('#sessionId').val();
    game.server.addUserToGame(sessionId, name);
    //game.server.getUserStatus(sessionId);
    //game.server.getCurrentPicker();
    gameManager.init();
});

var gameManager = {
    MAX_PLAYERS: 3, // Maximum number of player excluding Sherlock
    didPlayerWin : true,
    init : function(){
        console.log('gameManager.init');
        user.sessionID = $('#sessionId').val();
        game.server.startGame();

        /* GAME UI */

        $('#selection .answer .cell').click(function (event) {
            $(this).addClass("inactive");
            gameManager.handleChallengeSelection();
            gameManager.responseCount = 0;
        });

        $('#challenge').click(function (e) {
            gameManager.presentAnswer();
        });
        $('#response').click(function (e) {
            gameManager.dismissAnswer();
            gameManager.presentBoard();

            user.answerBox.val('');
            sherlock.answerBox.text('');
        });


        /* User UI */
        user.answerSubmit.click(function (e) {
            console.log('getting user answer');
            e.preventDefault();
            var userAnswerText = user.answerBox.val();
            console.log(userAnswerText);
            game.server.sendResponse(user.sessionID, userAnswerText);
        });

        /*  Tokbox */
        TB.addEventListener("exception", tok.exceptionHandler);

        if (TB.checkSystemRequirements() != TB.HAS_REQUIREMENTS) {
            alert('Minimum System Requirements not met!');
        }
        else {
            tok.session = TB.initSession(tok.sessionId);
            
            tok.session.addEventListener("sessionConnected", tok.sessionConnectedHandler);
            tok.session.addEventListener("streamCreated", tok.streamCreatedHandler);
            tok.session.connect(tok.apiKey, tok.token);
		
            //session.addEventListener('sessionDisconnected', sessionDisconnectedHandler);
            //session.addEventListener('connectionCreated', connectionCreatedHandler);
            //session.addEventListener('connectionDestroyed', connectionDestroyedHandler);
            //session.addEventListener('streamDestroyed', streamDestroyedHandler);
        }
        //*/

        gameManager.loop();
    },
    loop: function () {
        console.log('gameManager.loop');
        // 1 select question by player
        gameManager.presentBoard();

        // present
        // send question to sherlock
        // accept winner
        // select question by player
    },
    presentBoard : function(){
        $('#selection').addClass('show');
    },

    dismissBoard: function () {
        $('#selection').removeClass('show');
    },
    presentChallenge: function (challenge) {

        $('#challenge').html(challenge.Challenge).show();
        $('#response').html(challenge.CorrectResponse).hide();

        $('#presentor').removeClass('hide');
        setTimeout(function(){
            $('#presentor')[0].className = "";
        }, 100);
        setTimeout(function () {
            gameManager.dismissBoard();
        }, 350);

    },
    handleChallengeSelection : function(){
        game.server.sendChallengeByCategory('City');
        game.server.getSherlockResponses();
        
    },
    handlePlayerResponse: function (sessionId, response) {
        gameManager.responseCount++;

        if (gameManager.responseCount == 2) {
            
        }
    },
    responseCount : 0,
    checkAnswer : function(answer){
        if (gameManager.answerToCheck != $('#response').text()) {
            return true;
        }
    },

	presentAnswer : function (){
	    $('#challenge').hide();
	    $('#response').show();
	},

	dismissAnswer: function () {
	    $('#presentor').addClass('hide');
	},
};

var user = {
    answerBox: $('#playerAnswer'),
    answerSubmit: $('#playerSubmit'),
	canPlay : true,
	sessionID : "",
	streamID : "",
	declareTurn : function(){},
};




var sherlock = {
    answerBox : $('#sherlockAnswer'),
};


/** OPEN TOK **/
// TODO integrate with signalR controls
// Setup
var tok = {
	apiKey : '32135502', 
	sessionId: '1_MX4zMjEzNTUwMn4xMjcuMC4wLjF-VHVlIEp1biAxOCAxMzowMToxMCBQRFQgMjAxM34wLjM1MzQxMDA3fg',
	token: 'T1==cGFydG5lcl9pZD0zMjEzNTUwMiZzZGtfdmVyc2lvbj10YnJ1YnktdGJyYi12MC45MS4yMDExLTAyLTE3JnNpZz1mNjA4OWYyMzdhMTM5MTc1Yjg1ZDJmYmRhNjQ0OGI1MDA5YjdiYWVlOnJvbGU9cHVibGlzaGVyJnNlc3Npb25faWQ9JmNyZWF0ZV90aW1lPTEzNzE1ODU2NjUmbm9uY2U9MC4yMDQ4NTA1ODU5NTY0MDY0OCZleHBpcmVfdGltZT0xMzc0MTc3NjY1JmNvbm5lY3Rpb25fZGF0YT0=',
	VIDEO_WIDTH : 215,
	VIDEO_HEIGHT : 138,
	publisher: null,
    session: null,

	exceptionHandler : function (event) {
		alert("Exception: " + event.code + "::" + event.message);
	},
	
	// Tokbox Event Handlers
	sessionConnectedHandler : function (event) {
		if(event.streams.length < gameManager.MAX_PLAYERS){

			console.log('publish');
			tok.startPublishing();
			user.canPlay = false;
		}
		
		tok.subscribeToStreams(event.streams);
	},
	
	streamCreatedHandler : function (event) {
		tok.subscribeToStreams(event.streams);
	},
	
	// Tokbox UI
	subscribeToStreams : function (streams) {
		var avatarCount = 0;
		var allowableStreams = (gameManager.MAX_PLAYERS > streams.length) ? streams.length : gameManager.MAX_PLAYERS;

		console.log('gameManager.MAX_PLAYERS: ' + gameManager.MAX_PLAYERS);
		console.log('streams.length: ' + streams.length);
		console.log('allowableStreams: ' + allowableStreams);
			console.log('-------')

		// Check if already publishing
		if (tok.publisher){
			avatarCount = 1;
			allowableStreams--;
		}
		for (var i = 0; i < streams.length; i++) {
			console.log('iterator ' + i);

			// Limit streams to 3
			if (i >= gameManager.MAX_PLAYERS) {
				break;
			}

			var stream = streams[i];
			
			if (stream.connection.connectionId != tok.session.connection.connectionId) {

				console.log('avatarCount: ' + avatarCount);
				var parentDiv = document.getElementById("player_avatar_"+ avatarCount);
				var subscriberDiv = document.createElement('div'); // Create a div for the subscriber to replace

				subscriberDiv.setAttribute('id', stream.streamId); // Give the replacement div the id of the stream as its id.
				//parentDiv.appendChild(subscriberDiv);
				//var subscriberProps = {width: tok.VIDEO_WIDTH, height: tok.VIDEO_HEIGHT};
				//tok.session.subscribe(stream, subscriberDiv.id, subscriberProps);


				avatarCount ++;
			}
		}
	},

	startPublishing : function() {
			var parentDiv = document.getElementById("player_avatar_0");
			var publisherDiv = document.createElement('div'); // Create a div for the publisher to replace
			publisherDiv.setAttribute('id', 'opentok_publisher');
			parentDiv.appendChild(publisherDiv);
			var publisherProps = {width: tok.VIDEO_WIDTH, height: tok.VIDEO_HEIGHT, publishAudio: false};

			tok.publisher = TB.initPublisher(tok.apiKey, publisherDiv.id, publisherProps);  // Pass the replacement div id and properties
			tok.session.publish(tok.publisher);
	}
}





