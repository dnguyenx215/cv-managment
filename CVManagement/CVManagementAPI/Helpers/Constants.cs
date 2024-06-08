using System.Text.RegularExpressions;


namespace CVManagementAPI.Helpers
{
    public class Constants
    {
        public static class SubjectMail
        {
            public const string REVIEW_CV_FAIL = "Thông báo kết quả sơ loại hồ sơ";
            public const string REVIEW_CV_PASS = REVIEW_CV_FAIL;

            public const string INTERVIEW_RES_FAIL = "Thư cảm ơn - Tokyah Studio trân trọng cảm ơn bạn";
            public const string INTERVIEW_RES_PASS = INTERVIEW_RES_FAIL;
            public const string INTERVIEW_RES_BACKUP = INTERVIEW_RES_FAIL;
        }
        public static class ContentMail
        {
            public const string REVIEW_CV_FAIL = @"<p>Xin lỗi, bạn đã không được chọn</p><p></p><p>Chúng tôi vừa hoàn tất quá trình đánh giá các ứng viên cho vị trí <strong>PositionName</strong>, và rất lấy làm tiếc phải thông báo rằng bạn không được chọn để tiếp tục quá trình phỏng vấn.</p><p>Chúng tôi đã nhận được rất nhiều hồ sơ ứng tuyển xuất sắc, và việc lựa chọn ứng viên đã trở nên vô cùng khó khăn. Tuy nhiên, chúng tôi vẫn rất ấn tượng với hồ sơ của bạn và mong muốn được tiếp tục hợp tác trong tương lai.</p><p>Chúng tôi chân thành cảm ơn bạn đã quan tâm và nộp hồ sơ ứng tuyển với chúng tôi. Chúc bạn sẽ sớm tìm được vị trí phù hợp với kinh nghiệm và kỹ năng của mình.</p><p><a target=""_blank"" rel=""noopener noreferrer nofollow"" class="""" href=""https://example.com/job-openings""><em>Xem thêm các vị trí đang tuyển dụng</em></a></p>";

            public const string REVIEW_CV_PASS = @"<p>Thân gửi bạn,</p><p></p><p>Thay mặt công ty Tokyha Studio, chúng tôi trân trọng <strong>cảm ơn bạn</strong> đã quan tâm đến công việc và vị trí tuyển dụng tại công ty chúng tôi.</p><p>Chúng tôi <strong>đánh giá cao CV và kỹ năng</strong> của bạn. Chúng tôi hân hạnh được mời bạn tham gia buổi phỏng vấn như sau:</p><p><strong>1. Thời gian: TimeOfInterview-DateOfInterview</strong></p><p><strong>2. Địa điểm: AddressDetail</strong></p><p><strong>3. Vị trí: PositionName</strong></p><p>Bạn vui lòng <strong>xác nhận tham gia phỏng vấn</strong> bằng cách phản hồi lại email này.</p><p>Nếu có bất cứ khó khăn hoặc câu hỏi nào, xin vui lòng liên hệ email này hoặc <strong>chị Hạnh, ĐT 034 616 27 08</strong></p><p>Cảm ơn bạn và hẹn gặp bạn tại buổi phỏng vấn.</p><p>Trân trọng,</p><p></p><p><em>Hướng dẫn:</em></p><p><em>Bạn đi đến Tòa nhà Sun Square ngay ngã tư Lê Đức Thọ - Nguyễn Hoàng.</em></p><p><em>Bạn xuống tầng hầm, lấy vé gửi xe, vào thang máy, lên sảnh tầng 1 ngồi nghỉ.</em></p><p><em>Gọi điện thoại cho chị Hạnh xuống đón.</em></p><p><a target=""_blank"" rel=""noopener noreferrer nofollow"" class="""" href=""https://www.google.com/maps/place/2QMC%2BF3V,+Nam+T%E1%BB%AB+Li%C3%AAm,+H%C3%A0+N%E1%BB%99i,+Vietnam/@21.0337425,105.767597,17z/data=!3m1!4b1!4m5!3m4!1s0x313454bab9b67e93:0xbf7faf935966a683!8m2!3d21.0337375!4d105.7701719?entry=ttu""><em>Tòa nhà Sun Square - Google Maps</em></a></p>";

            public const string INTERVIEW_RES_FAIL = @"<p>Thân gửi bạn,</p><p></p><p>Thay mặt công ty Tokyha Studio, chúng tôi trân trọng <strong>cảm ơn bạn</strong> đã tham gia phỏng vấn.</p><p>Thông qua quá trình phỏng vấn, chúng tôi đánh giá cao kiến thức và kỹ năng của bạn, tuy nhiên, nó là chưa phù hợp với vị trí và yêu cầu ứng tuyển.</p><p><strong>Chúng tôi rất tiếc khi không thể đồng hành cùng bạn trong thời gian tới.</strong></p><p>Chúng tôi tin tưởng rằng kiến thức và kỹ năng của bạn sẽ phát triển hơn trong tương lai.</p><p>Hi vọng được đồng hành với bạn ở thời điểm thích hợp.</p><p></p><p>Một lần nữa Tokyha xin cảm ơn bạn!</p><p></p><p>Thanks/</p>";

            public const string INTERVIEW_RES_PASS = @"<p>Thân gửi bạn,</p><p></p><p>Thay mặt Tokyha Studio, chúng tôi trân trọng <strong>cảm ơn bạn</strong> đã tham gia phỏng vấn. Chúng tôi xin <strong>chúc mừng bạn</strong> đã <strong>trúng tuyển</strong> tại công ty chúng tôi. Thời gian, vị trí và mức hỗ trợ cụ thể:</p><p><strong>1. Vị trí: PositionName</strong></p><p><strong>2. Mức hỗ trợ: </strong><span><strong>Salary</strong></span></p><p><strong>3. Ngày bắt đầu làm việc:dateAcceptJob</strong></p><p><strong>4. Thời gian làm việc:</strong></p><p><strong>- Sáng : 08h00 - 12h00</strong></p><p><strong>- Chiều: 13h30- 17h30</strong></p><p><strong>- Làm từ thứ 2 đến thứ 6, Chủ Nhật nghỉ, mỗi tháng làm so le 2 ngày thứ 7, các ngày thứ 7 còn lại nghỉ.</strong></p><p><strong>5. Địa chỉ: AddressDetail</strong></p><p>Bạn vui lòng xác nhận bằng cách phản hồi lại email này trong vòng 48h.</p><p>Nếu có bất cứ khó khăn hoặc câu hỏi nào, xin vui lòng liên hệ email này hoặc chị Hạnh, ĐT 034 616 2708.</p><p>Chú ý: Nhớ mang theo laptop khi tham gia thực tập nhé.</p><p>Cảm ơn bạn và hẹn gặp bạn tại công ty.</p>";

            public const string INTERVIEW_RES_BACKUP = @"<h1>Chúc mừng!</h1><p>Chúng tôi vui mừng thông báo rằng bạn đã được chọn để tham gia vào vị trí <strong>PositionName</strong> tại công ty của chúng tôi.</p><p>Tuy nhiên, do một số vấn đề nội bộ mà công ty đang giải quyết, việc bạn bắt đầu công việc sẽ phải tạm hoãn lại một thời gian.</p><p>Chúng tôi sẽ liên lạc với bạn trong vòng 2 tuần để cập nhật thông tin mới nhất và báo lại ngày bạn có thể bắt đầu làm việc.</p><p>Chúng tôi rất mong được làm việc cùng bạn và trông đợi những đóng góp tuyệt vời của bạn cho công ty. Nếu bạn có bất kỳ câu hỏi hoặc thắc mắc nào, vui lòng liên hệ với chúng tôi.</p><p><a target=""_blank"" rel=""noopener noreferrer nofollow"" class="""" href=""https://example.com/contact-us"">Liên hệ với chúng tôi</a></p>";

        }

        public static class Gmail
        {
            public const string CC_DEFAULT = "lenguyenhaianh0808@gmail.com";
            public const string BCC_DEFAULT = "BCC";
        }
        public static class StatusCv
        {
            public const string REVIEW_CV_WAITING = "Đang chờ xem xét hồ sơ";
            public const string REVIEW_CV_FAIL = "Không đủ điều kiện";
            public const string REVIEW_CV_PASS = "Đủ điều kiện";
            public const string REFUSED_TO_BE_INTERVIEWED = "Từ chối phỏng vấn";
            public const string INTERVIEW_RES_FAIL = "Không đạt phỏng vấn";
            public const string INTERVIEW_RES_PASS = "Đạt phỏng vấn";
            public const string REFUSE_TO_ACCEPT_JOB = "Từ chối nhận việc";
            public const string GET_JOB = "Đã nhận việc";
            public const string INTERVIEW_RES_BACKUP = "Kết quả phỏng vấn dự phòng";
            public const string NOT_SENT = "NOT_SENT";
            public const string SENDING = "SENDING";
            public const string SENT = "SENT";
        }

        public static class EmailStatus
        {
            public const string NOT_SENT = "NOT_SENT";
            public const string SENDING = "SENDING";
            public const string SENT = "SENT";
        }
        public static class Address
        {
            public const string AddressDetail = "Tòa B phòng 27B5 ,Tòa Sun Square, 21 Lê Đức Thọ, Mỹ Đình 2, Nam Từ Liêm, Hà Nội.";
        }

        public static class Surname
        {
            public static List<string> list = new List<string>()
            {
                "Nguyễn","Trần","Lê","Phạm","Hoàng","Huỳnh","Vũ","Võ","Phan","Trương","Bùi","Đặng","Đỗ","Ngô","Hồ","Dương","Đinh","Lý","Ái", "An", "Anh", "Ao", "Ánh", "Ân", "Âu", "Âu Dương", "Ấu", "Bá", "Bạc", "Ban", "Bạch", "Bàn", "Bàng", "Bành", "Bảo", "Bế", "Bì", "Biện", "Bình", "Bồ", "Chriêng", "Ca", "Cà", "Cái", "Cai", "Cam", "Cảnh", "Cao", "Cáp", "Cát", "Cầm", "Cấn", "Chế", "Chiêm", "Chu", "Chắng", "Chung", "Chúng", "Chương", "Chử", "Cồ", "Cổ", "Công", "Cống", "Cung", "Cù", "Cự", "Dã", "Danh", "Diêm", "Diếp", "Doãn", "Diệp", "Du", "Duy", "Dư", "Đái", "Đan", "Đàm", "Đào", "Đăng", "Đắc", "Đầu", "Đậu", "Đèo", "Điêu", "Điền", "Điều", "Đinh", "Đình", "Đoái", "Đoàn", "Đoạn", "Đôn", "Đống", "Đồ", "Đồng", "Đổng", "Đới", "Đương", "Đường", "Đức", "Giả", "Giao", "Giang", "Giàng", "Giản", "Giảng", "Giáp", "Hưng", "H'", "H'ma", "H'nia", "Hầu", "Hà", "Hạ", "Hàn", "Hàng", "Hán", "Hề", "Hình", "Hoa", "Hoà", "Hoài", "Hoàng Phủ", "Hồng", "Hùng", "Hứa", "Hướng", "Hy", "Kinh", "Kông", "Kiểu", "Kha", "Khà", "Khai", "Khâu", "Khiếu", "Khoa", "Khổng", "Khu", "Khuất", "Khúc", "Khương", "Khưu", "Kiều", "Kim", "Ly", "Lý", "La", "Lã", "Lành", "Lãnh", "Lạc", "Lại", "Lăng", "Lâm", "Lầu", "Lèng", "Lều", "Liên", "Liệp", "Liêu", "Liễu", "Linh", "Loan", "Long", "Lò", "Lô", "Lỗ", "Lộ", "Lộc", "Luyện", "Lục", "Lù", "Lư", "Lương", "Lường", "Lưu", "Ma", "Mai", "Man", "Mang", "Mã", "Mê", "Mạc", "Mạch", "Mạnh", "Mâu", "Mậu", "Mầu", "Mẫn", "Minh", "Mộc", "Mông", "Mùa", "Mục", "Miêu", "Mễ", "Niê", "Ngạc", "Ngân", "Nghiêm", "Nghị", "Ngọ", "Ngọc", "Ngôn", "Ngũ", "Ngụy", "Nhan", "Nhâm", "Nhữ", "Ninh", "Nông", "Ong", "Ô", "Ông", "Phi", "Phí", "Phó", "Phong", "Phù", "Phú", "Phùng", "Phương", "Quản", "Quán", "Quang", "Quàng", "Quảng", "Quách", "Quế", "Quốc", "Quyền", "Sái", "Sâm", "Sầm", "Sơn", "Sử", "Sùng", "Sỳ", "Tán", "Tào", "Tạ", "Tăng", "Tấn", "Tất", "Tề", "Thang", "Thanh", "Thái", "Thành", "Thào", "Thạch", "Thân", "Thẩm", "Thập", "Thế", "Thi", "Thiều", "Thiệu", "Thịnh", "Thiềm", "Thoa", "Thôi", "Thóng", "Thục", "Tiêu", "Tiết", "Tiếp", "Tinh", "Tòng", "Tô", "Tôn", "Tôn Nữ", "Tôn Thất", "Tông", "Tống", "Trang", "Tráng", "Trác", "Trà", "Trâu", "Tri", "Trì", "Triệu", "Trình", "Trịnh", "Trung", "Trưng", "Tuấn", "Từ", "Tưởng", "Tướng", "Ty", "Uông", "Uân", "Ung", "Ưng", "Ứng", "Vàng", "Vâng", "Vạn", "Văn", "Văng", "Vi", "Vĩnh", "Viêm", "Viên", "Việt", "Vòng", "Vừ", "Vương", "Vưu", "Vu", "Xa", "Xung", "Y", "Yên", "Hầu", "Lương"
            };
            public static string RemoveDiacritics(string input)
            {
                input = Regex.Replace(input, "[àáảãạâầấẩẫậăằắẳẵặ]", "a");
                input = Regex.Replace(input, "[èéẻẽẹêềếểễệ]", "e");
                input = Regex.Replace(input, "[ìíỉĩị]", "i");
                input = Regex.Replace(input, "[òóỏõọôồốổỗộơờớởỡợ]", "o");
                input = Regex.Replace(input, "[ùúủũụưừứửữự]", "u");
                input = Regex.Replace(input, "[ỳýỷỹỵ]", "y");
                return input;
            }
            public static string StringRegex()
            {
                var result = "";
                for (int i = 0; i < list.Count; i++)
                {
                    result += list[i] + "|" + RemoveDiacritics(list[i]);
                    if (i < list.Count - 1)
                        result += "|";
                }
                return result;
            }
        }

        /// <summary>
        /// Thời gian hết hạn của session, file gửi lên từ client
        /// </summary>
        public const int ExpiredTime = 1;
    }
}
