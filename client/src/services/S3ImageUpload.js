import Storage from "aws-amplify";

class S3ImageUpload {
  uploadImage(e, type) {  //type = avatar/background
    console.log(e);
    const file = e;
    Storage.put(type + "/" + file.name, file, {
      contentType: "image/png"
    })
      .then(result => console.log(result))
      .catch(err => console.log(err));
  }
  

}

export default new S3ImageUpload();
