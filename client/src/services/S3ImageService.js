import { Storage } from "aws-amplify";

class S3ImageService {
  uploadImage(e, type) {
    //type = avatar or background
    console.log(e);
    const file = e;
    const test = Storage;
    const path = type + "/" + file.name;
    // console.log(test);
    // debugger;
    test.put(path, file, {
        level: "protected",
        contentType: "image/png"
      })
      .then(result => {
        console.log(result);
      })
      .catch(err => console.log(err));
  }
  async listImages(type) {
    const path = type + "/";
     const res = await Storage.list(path) // for listing ALL files without prefix, pass '' instead
      // .then(result => {
      //   t=result;
      // })
      // .catch(err => console.log(err));
      return res
  }
  async  removeImage(key){
   const res = await Storage.remove(key, { level: 'protected' })
    return res;
  }
  async getImgByKey(key) {
    let image = await Storage.get(key);
    return image;
  }
}

export default new S3ImageService();
