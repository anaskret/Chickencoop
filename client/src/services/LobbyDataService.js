import http from "../http-common";

class LobbyDataService{
    getAll(){
        return http.get("/lobbies");
    }

    get(id) {
        return http.get(`/lobbies/${id}`);
    }

    create(data) {
        return http.post("/lobbies", data);
    }

    update(id, data) {
        return http.put(`/lobbies/${id}`, data);
    }

    delete(id) {
        return http.delete(`/lobbies/${id}`);
    }
}

export default new LobbyDataService();