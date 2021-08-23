const DateTime = {
    formatar(data) {
        if (!data)
            return null;
            
        return new Date(data)
            .toISOString()
            .split("T")[0];
    }
};

export default DateTime;
