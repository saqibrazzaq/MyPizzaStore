import Api from "../Api/Api";
import UserLoginDto from "../Models/User/UserLoginDto";

const url = "User";

export function login(data: UserLoginDto) {
  // console.log('data posted to api')
  // console.log(data);
  return Api.post(url + "/login", data);
}

export function logout() {
  return Api.post(url + "/logout");
}