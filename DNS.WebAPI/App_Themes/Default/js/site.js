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

	$( '.control audio' ).mediaelementplayer({
		loop: true,
        shuffle: false,
        audioHeight: 30, 
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

	/* Bible Plan on mobile device */
	if( $(window).width() <= 768 ) {
		$( '#bible-plan .bible-plan-day .day-title' ).on( 'click', function() {
			$( '#bible-plan .bible-plan-day ul' ).hide();

			$( '#bible-plan .bible-plan-day .day-title' ).removeClass( 'active' );
			$( this ).addClass('active');

			$( this ).next( 'ul' )
				.addClass('mobile')
				.show()
				.append('<span class="close-plan"><i class="icon-cross"></i> close</span>')
		});

		$( '#bible-plan .bible-plan-day' ).delegate( '.close-plan', 'click', function() {
			$( this ).parent( 'ul' ).hide();
			$( this ).parent( 'ul' ).prev('.day-title').removeClass('active');
		});
	}

	
});