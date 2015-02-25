$( document ).ready( function (){
	$( '#home-audio' ).mediaelementplayer({
		loop: true,
        shuffle: false,
        playlist: true,
        audioHeight: 30,
        playlistposition: 'bottom',
        features: ['playlistfeature', 'prevtrack', 'playpause', 'nexttrack', 'playlist', 'current', 'progress', 'duration', 'volume'],
        keyActions: []
	});

	/* Remove show/hide toggle button and time loaded */
	$( '.mejs-hide-playlist, .mejs-time-loaded' ).remove();
	
	/* Add order to playlist */
	var order = 1;
	$( '.control .mejs-playlist li' ).each( function() {
		$( this ).prepend( '<span class="number">0' + order++ + '. </span>' );	
	} );

	/* Set same height of day on bible plan */
	$( '.bible-plan-week' ).each( function() {
		var max = 0;
		$( this ).find( '.bible-plan-day' ).each( function() {
			if( $( this ).height() > max ) {
				max = $( this ).height();
			}
		});

		$( this ).find( '.bible-plan-day' ).height( max );
	});

	/* Show/hide list month */
	$( '.select-month .current-month' ).on( 'click', function(event) {
		var listMonth = $( this ).next( '.list-month' );
		if( listMonth.is(':hidden') ) {
			listMonth.show();
		} else {
			listMonth.hide();
		}

		event.stopPropagation();
	});

	$( 'body' ).on( 'click', function() {
		if( $( '.list-month' ).is(':not(:hidden)') ){
			$( '.list-month' ).hide();
		}
	});
});