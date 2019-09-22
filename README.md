


API Documentation

Other than the authentication endpoints, output of all other endpoints have the following structure
Statuscode
Status
Data



------
Images
-------

---upload --
endpoint : api/image/upload
method : post
authenticated : true
params : Title [string], Caption [text], Location [string], NumberOfPrint [int], ImageFile [file]


--- filter----
endpoint : api/filter/images 
method : get
authenticated : false

params : Type [string], Start [int], Count [int]
	{{Type} = random,... }

-- single image----
endpoint : api/image/{imageId} 
	{{imageId} is PikchaImageId property of the image }
method : get
authenticated : false

params : {imageId}

-- tags--
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
