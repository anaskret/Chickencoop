import {Storage} from "aws-amplify";

class S3ImageUpload {
  uploadImage(e, type) {
    //type = avatar or background
    console.log(e);
    const file = e;
    const test = Storage;
    const path = type + "/" + file.name;
    // console.log(test);
    // debugger;
    test.put(path, file, {
      contentType: "image/png"
    })
      .then(result => {
        debugger;
        console.log(result);
      })
      .catch(err => console.log(err));
  }
}

export default new S3ImageUpload();
