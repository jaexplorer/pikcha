


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
- ENDPOINT : api/filter/images 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type [string, Optional], Start [int, Optional], Count [int, Optional]
	{{Type} = random,... }
- RESULTS : images[]

### Get an Image
- ENDPOINT : api/image/{imageId} 
- METHOD : get
- AUTHENTICATED : false

- RESULTS : image


### upload
- ENDPOINT : api/image/upload
- METHOD : post
- AUTHENTICATED : true
- PARAMS : Title [string], Caption [text], Location [string], NumberOfPrint [int], ImageFile [file]
- RESULTS : image


### tags
- ENDPOINT : api/image/tags
- METHOD : get
- AUTHENTICATED : false
- RESULTS : tags[]



# Artists

### Filter Images
- ENDPOINT : api/filter/artists 
- METHOD : get
- AUTHENTICATED : false
- QUERY_PARAMS : Type [string, Optional], Start [int, Optional], Count [int, Optional]
	{{Type} = random,... }
- RESULTS : artists[]


### Get an Artist
- ENDPOINT : api/profile/{userId} 
- METHOD : get
- AUTHENTICATED : false

- RESULTS : artist
