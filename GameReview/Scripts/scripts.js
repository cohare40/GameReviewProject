$(document).ready(function () {
    //Carousel JS
    $('#gameCarousel').slick({
        infinite: true,
        slidesToShow: 3,
        slidesToScroll: 3,
        autoplay: true,
        autoplaySpeed: 5000,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

    $("#navbarSearchSubmit").click(function(event){
        event.preventDefault();
        window.location.href = $("#searchId").val() + '?searchFilter=' +  $("#searchBarNav").val() + '&searchType=name';
    });

    var $j = jQuery.noConflict();
    $j('.summaryText').each(function() {
        var $pTag = $j(this).find('p');
        if($pTag.text().length > 300){
            var shortText = $pTag.text();
            shortText = shortText.substring(0, 300);
            $pTag.addClass('fullArticle').hide();
            $pTag.append('</p><a class="read-less-link text-white">Read Less</a>');
            $j(this).append('<p class="preview text-white">'+shortText+'</p><div class="curtain-shadow"></div><a class="read-more-link text-white">Read more</a>');
        }
    });

    $j(document).on('click', '.read-more-link', function () {
        $j(this).hide().parent().find('.preview').slideUp().prev().slideDown();
    });

    $j(document).on('click', '.read-less-link', function () {
        $j(this).parent().slideUp().next().show();
        $j(this).parents('.summaryText').find('.read-more-link').slideDown(300);
    });

    var ctx = document.getElementById('myChart').getContext('2d');
    var chart = new Chart(ctx, {
        // The type of chart we want to create
        type: 'line',

        // The data for our dataset
        data: {
            labels: ['*', '**', '***', '****', '*****'],
            datasets: [{
                label: 'My First dataset',
                backgroundColor: 'rgb(255, 99, 132)',
                borderColor: 'rgb(255, 99, 132)',
                data: [0, 10, 5, 2, 20, 30, 45]
            }]
        },

        // Configuration options go here
        options: {}
    });
});