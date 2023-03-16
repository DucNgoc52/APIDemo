namespace WebAPIClone.Commom.MSG
{
    public static class MsgError
    {
        public const string LOGIN_FAILED = "Email hoặc mật khẩu không chính xác";
        public const string SIGNIN_FAILED = "Email đã tồn tại";
        public const string PASS_NOT_VALID = "Mật khẩu chưa đủ mạnh";

        public const string ITEM_CREATE_FAILED = "Tạo mới thất bại";
        public const string ITEM_UPDATE_FAILED = "Cập nhật thất bại";
        public const string ITEM_DELETE_FAILED = "Xóa thất bại";
        public const string GET_ITEM_byID_FAILED = "Dữ liệu không tồn tại";
        public const string GET_ITEM_FAILED = "Lấy dữ liệu thất bại";
        public const string ITEM_DUPLICATE_ID = "Id này đã tồn tại";
        public const string ID_DOESNT_EXITS = "Id không tồn tại";
    }
}
