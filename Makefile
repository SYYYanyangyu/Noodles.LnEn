FILESERVICENAME = fileservice
IDENTITYSERVICENAME = identityservice
LISTENADMINSERVICENAME = listenadminservice
LISTENMAINSERVICENAME = listenmainservice
MEDIAENCODERSERVICENAME = mediaencoderservice
SEARCHSERVICENAME = searchservice
VERSION = $(date +'%Y%m%d%H%M')

FILESERVICEPORT = 9001
IDENTITYSERVICEPORT = 9002
LISTENADMINSERVICEPORT = 9003
LISTENMAINSERVICEPORT = 9004
MEDIAENCODERSERVICEPORT = 9005
SEARCHSERVICENAME = 9006

.PHONY: run-identityservice build-identityservice tag-backup-identityservice \
run-fileservice build-fileservice tag-backup-fileservice \
run-listenadminservice build-listenadminservice tag-backup-listenadminservice \
run-listenmainservice build-listenmainservice tag-backup-listenmainservice    \
run-mediaencoderservice build-mediaencoderservice tag-backup-mediaencoderservice  \
run-searchservice build-searchservice tag-backup-searchservice

all : run-identityservice

######################################################################
#              FileService   文件服务                                #
######################################################################
run-fileservice:build-fileservice
	- docker run -d --rm -p ${FILESERVICEPORT}:80 ${FILESERVICENAME} --name ${FILESERVICENAME}

build-fileservice:tag-backup-fileservice
	- docker build -t ${FILESERVICENAME} -f FileService.WebAPI/Dockerfile .

tag-backup-fileservice: stop-fileservice
	- docker tag ${FILESERVICENAME}:latest ${FILESERVICENAME}:${VERSION}

stop-fileservice:
	- docker stop ${FILESERVICENAME}

######################################################################
#              IdentityService   登录认证服务                        #
######################################################################

run-identityservice:build-identityservice
	- docker run -d --rm -p ${IDENTITYSERVICEPORT}:80 ${IDENTITYSERVICENAME} --name ${IDENTITYSERVICENAME}

build-identityservice:tag-backup-identityservice
	- docker build -t ${IDENTITYSERVICENAME} -f IdentityService.WebAPI/Dockerfile .

tag-backup-identityservice: stop-identityservice
	- docker tag ${IDENTITYSERVICENAME}:latest ${IDENTITYSERVICENAME}:${VERSION}

stop-identityservice:
	- docker stop ${IDENTITYSERVICENAME}
	
######################################################################
#              ListenAdminService   听力后台管理服务                 #
######################################################################
run-listenadminservice:build-listenadminservice
	- docker run -d --rm -p ${LISTENADMINSERVICEPORT}:80 ${LISTENADMINSERVICENAME} --name ${LISTENADMINSERVICENAME}

build-listenadminservice:tag-backup-listenadminservice
	- docker build -t ${LISTENADMINSERVICENAME} -f Listening.Admin.WebAPI/Dockerfile .

tag-backup-listenadminservice: stop-listenadminservice
	- docker tag ${LISTENADMINSERVICENAME}:latest ${LISTENADMINSERVICENAME}:${VERSION}

stop-listenadminservice:
	- docker stop ${LISTENADMINSERVICENAME}
	
######################################################################
#              ListenMainService   听力前端展示服务                  #
######################################################################
run-listenmainservice:build-listenmainservice
	- docker run -d --rm -p ${LISTENMAINSERVICEPORT}:80 ${LISTENMAINSERVICENAME} --name ${LISTENMAINSERVICENAME}

build-listenmainservice:tag-backup-listenmainservice
	- docker build -t ${LISTENMAINSERVICENAME} -f Listening.Main.WebAPI/Dockerfile .

tag-backup-listenmainservice: stop-listenmainservice
	- docker tag ${LISTENMAINSERVICENAME}:latest ${LISTENMAINSERVICENAME}:${VERSION}

stop-listenmainservice:
	- docker stop ${LISTENMAINSERVICENAME}
	
######################################################################
#              MediaEncoderService   解码服务                        #
######################################################################	
run-mediaencoderservice:build-mediaencoderservice
	- docker run -d --rm -p ${MEDIAENCODERSERVICEPORT}:80 ${MEDIAENCODERSERVICENAME} --name ${MEDIAENCODERSERVICENAME}

build-mediaencoderservice:tag-backup-mediaencoderservice
	- docker build -t ${MEDIAENCODERSERVICENAME} -f MediaEncoder.WebAPI/Dockerfile .

tag-backup-mediaencoderservice: stop-mediaencoderservice
	- docker tag ${MEDIAENCODERSERVICENAME}:latest ${MEDIAENCODERSERVICENAME}:${VERSION}

stop-mediaencoderservice:
	- docker stop ${MEDIAENCODERSERVICENAME}
	
######################################################################
#              SearchService   搜索服务                              #
######################################################################	
run-searchservice:build-searchservice
	- docker run -d --rm -p ${SEARCHSERVICENAME}:80 ${SEARCHSERVICENAME} --name ${SEARCHSERVICENAME}

build-searchservice:tag-backup-searchservice
	- docker build -t ${SEARCHSERVICENAME} -f SearchService.WebAPI/Dockerfile .

tag-backup-searchservice: stop-searchservice
	- docker tag ${SEARCHSERVICENAME}:latest ${SEARCHSERVICENAME}:${VERSION}

stop-searchservice:
	- docker stop ${SEARCHSERVICENAME}
