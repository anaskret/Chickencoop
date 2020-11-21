import http from "../http-common";

class PersonalLeaderboardDataService{
    getAll(){
        return http.get("/records");
    }
    
    getAllByPlayerId(id){
        return http.get(`/records/player/${id}`);
    }

    getRanking(){
        return http.get("/ranking");
    }

    get(id) {
        return http.get(`/records/${id}`);
    }

    create(data) {
        return http.post("/records", data);
    }

    update(id, data) {
        return http.put(`/records/${id}`, data);
    }

    delete(id) {
        return http.delete(`/records/${id}`);
    }
}

export default new PersonalLeaderboardDataService();