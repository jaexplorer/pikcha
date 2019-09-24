


# API Documentation

Other than the authentication endpoints, output of all other endpoints have the following structure
[ Statuscode="", Status="", Data=""]
for eg. successfull query [ Statuscode="200", Status="Success", Data= {Results Set}]
failure query [ Statuscode="1222", Status="Null Exception Occured", Data= ""]


* isauthenticated means whether the user needs to login to access the end point



# Images

Image properties
- PikchaImageId
- Title
- Caption
- Location
- NumberOfPrint
- Width
- Height
- ThumbnailFile
- WatermarkedFile
- UploadedAt
- Artist



### Filter Images
ENDPOINT : api/filter/images 
METHOD : get
AUTHENTICATED : false
QUERY_PARAMS : Type [string, Optional], Start [int, Optional], Count [int, Optional]
	{{Type} = random,... }
RESULTS : images[]

### Get an Image
ENDPOINT : api/image/{imageId} 
METHOD : get
AUTHENTICATED : false

RESULTS : image




# TO DO : following


### upload
endpoint : api/image/upload
method : post
authenticated : true
params : Title [string], Caption [text], Location [string], NumberOfPrint [int], ImageFile [file]


### filter
endpoint : api/filter/images 
method : get
authenticated : false

params : Type [string], Start [int], Count [int]
	{{Type} = random,... }

### single image
endpoint : api/image/{imageId} 
	{{imageId} is PikchaImageId property of the image }
method : get
authenticated : false

params : {imageId}

### tags
endpoint : api/image/tags
method : get
authenticated : false


------
Artists
--------
--- filter----
endpoint : api/filter/artists 
method : get
authenticated : false

params : Type [string], Start [int], Count [int]
	{{Type} = random,... }


-- single artist
endpoint : api/profile/{userId} 
	{ {userId} is userId property of the artists }
method : get
authenticated : false

params : {userId}
