import http from "../http-common";

class PlayerDataService {
  getAll() {
    return http.get("/players");
  }

  get(username) {
    return http.get(`/players/${username}`);
  }

  getById(id) {
    return http.get(`/playersById/${id}`);
  }

  create(data) {
    return http.post("/players", data);
  }

  update(id, data) {
    return http.put(`/players/${id}`, data);
  }

  delete(id) {
    return http.delete(`/players/${id}`);
  }

  checkForUpdate() {
    return http.post("checkPlayers");
  }
}

export default new PlayerDataService();
