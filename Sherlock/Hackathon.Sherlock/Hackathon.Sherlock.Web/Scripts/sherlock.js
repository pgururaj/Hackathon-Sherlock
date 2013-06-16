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
    game.server.getUserStatus(sessionId);
    game.server.getCurrentPicker();
    gameManager.init();
});

var gameManager = {
    MAX_PLAYERS: 3, // Maximum number of player excluding Sherlock

	init : function(){
		

	    // Check if hit max players

		// Get SESSION
		// Send session

		/*  Tokbox */
		TB.addEventListener("exception", tok.exceptionHandler);

		if (TB.checkSystemRequirements() != TB.HAS_REQUIREMENTS) {
		    alert('Minimum System Requirements not met!');
		}
		else {
			session = TB.initSession(tok.sessionId);
			session.addEventListener("sessionConnected", tok.sessionConnectedHandler);
			session.addEventListener("streamCreated", tok.streamCreatedHandler);
			session.connect(tok.apiKey, tok.token);
		
			//session.addEventListener('sessionDisconnected', sessionDisconnectedHandler);
			//session.addEventListener('connectionCreated', connectionCreatedHandler);
			//session.addEventListener('connectionDestroyed', connectionDestroyedHandler);
			//session.addEventListener('streamDestroyed', streamDestroyedHandler);
		
		}
		//*/
	},

	presentChallenge : function(){

		var challenge = "This 19th hermit won the superbowl";
		var response = "Julius Cesear Chavez";
		$('#challenge').text(challenge);
		$('#response').text(response).hide();

		document.getElementById('presentor').classList.remove('hide');
		setTimeout(function(){
			document.getElementById('presentor').className = "";
		},100);
	},
	registerBoardChoice : function(){

	},
};

var user = {
	canPlay : false,
	sessionID : "",
	streamID : "",
	askName : function(){},
	declareTurn : function(){},
};




var sherlock = {
	readChallenge : function(){},
	selectChallenge : function(){},
	submitResponse : function(){},
};


/** OPEN TOK **/
// TODO integrate with signalR controls
// Setup
var tok = {
	apiKey : '32135502', 
	sessionId: '2_MX4zMjEzNTUwMn4xMjcuMC4wLjF-U2F0IEp1biAxNSAxMDoxNTo1OSBQRFQgMjAxM34wLjMyMTE5fg', 
	token: 'T1==cGFydG5lcl9pZD0zMjEzNTUwMiZzZGtfdmVyc2lvbj10YnJ1YnktdGJyYi12MC45MS4yMDExLTAyLTE3JnNpZz0zNmI1ZTA4NTRlYjZjZmI1MmRiODA5MzBiMTE2Mzk1N2QxNWQ4ZmUyOnJvbGU9cHVibGlzaGVyJnNlc3Npb25faWQ9Ml9NWDR6TWpFek5UVXdNbjR4TWpjdU1DNHdMakYtVTJGMElFcDFiaUF4TlNBeE1Eb3hOVG8xT1NCUVJGUWdNakF4TTM0d0xqTXlNVEU1ZmcmY3JlYXRlX3RpbWU9MTM3MTMxNjU2MSZub25jZT0wLjc4MTQyMjE5Mjc4Mjg4MDMmZXhwaXJlX3RpbWU9MTM3MTQwMjk2MiZjb25uZWN0aW9uX2RhdGE9',
	VIDEO_WIDTH : 220,
	VIDEO_HEIGHT : 150,
	publisher : null,

	exceptionHandler : function (event) {
		alert("Exception: " + event.code + "::" + event.message);
	},
	
	// Tokbox Event Handlers
	sessionConnectedHandler : function (event) {
		if(event.streams.length < game.MAX_PLAYERS){

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
		var allowableStreams = (game.MAX_PLAYERS > streams.length)?streams.length:game.MAX_PLAYERS;

		console.log('game.MAX_PLAYERS: ' + game.MAX_PLAYERS);
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
			if(i >= game.MAX_PLAYERS){
				break;
			}

			var stream = streams[i];
			
			if (stream.connection.connectionId != session.connection.connectionId) {

				console.log('avatarCount: ' + avatarCount);
				var parentDiv = document.getElementById("player_avatar_"+ avatarCount);
				var subscriberDiv = document.createElement('div'); // Create a div for the subscriber to replace

				subscriberDiv.setAttribute('id', stream.streamId); // Give the replacement div the id of the stream as its id.
				parentDiv.appendChild(subscriberDiv);
				var subscriberProps = {width: tok.VIDEO_WIDTH, height: tok.VIDEO_HEIGHT};
				session.subscribe(stream, subscriberDiv.id, subscriberProps);


				avatarCount ++;
			}
		}
	},

	startPublishing : function() {
		if (!tok.publisher && user.canPlay) {
			var parentDiv = document.getElementById("player_avatar_0");
			var publisherDiv = document.createElement('div'); // Create a div for the publisher to replace
			publisherDiv.setAttribute('id', 'opentok_publisher');
			parentDiv.appendChild(publisherDiv);
			var publisherProps = {width: tok.VIDEO_WIDTH, height: tok.VIDEO_HEIGHT, publishAudio: false};

			tok.publisher = TB.initPublisher(tok.apiKey, publisherDiv.id, publisherProps);  // Pass the replacement div id and properties
			session.publish(tok.publisher);
		}
	}
}





