

$(() => {
    
    $("#like-button").on('click', function () {
        const id = $("#image-id").val()
        $.post(`/home/imagelikes?id=${id}`, function () {
            getNumOfLikesForImage(id)
                      
        })
        $(this).prop('disabled', true)
        setInterval(function () {
            const id = $('#image-id').val()
            getNumOfLikesForImage(id)
        }, 1000);
    })
    function getNumOfLikesForImage(id) {
       $.get(`/home/getlikesbyid?id=${id}`, function (likes) {
            $("#likes-count").text(likes)
        })
    }
 
  
  
})