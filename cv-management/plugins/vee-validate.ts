import { defineRule, configure } from 'vee-validate';
import { min, required } from '@vee-validate/rules';

export default defineNuxtPlugin(() => {
    configure({
        generateMessage: (ctx: any) => {
            const messages: any = {
                required: `Thông tin này là bắt buộc!`,
                min: `Trường ${ctx.field} phải có ít nhất ${ctx.rule.params[0]} ký tự!`,
                numeric: `Trường ${ctx.field} phải là một số hợp lệ!`,
            };

            return messages[ctx.rule.name];
        },
    });

    defineRule('required', required);
    defineRule('min', min);

    defineRule('email', (value: string) => {
        if (!value || !value.length) {
            return true;
        }
        if (!/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(value)
        ) {
            return 'Vui lòng nhập một email hợp lệ.';
        }
        return true;
    });
    defineRule('minLength', (value: string, [limit = 0]) => {
        // The field is empty so it should pass
        if (!value || !value.length) {
            return true;
        }
        if (value.length < limit) {
            return `This field must be at least ${limit} characters`;
        }
        return true;
    });
    defineRule('password', (value: string) => {
        // Password must have at least one uppercase letter, one lowercase letter, one number, and one special character
        const pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$/;
        return pattern.test(value) || 'Mật khẩu phải có ít nhất 5 ký tự bao gồm chữ hoa, chữ thường và số.';
    });
    defineRule('phone', (value: string) => {
        // Vietnamese phone number pattern
        const pattern = /((03|05|07|08|09|01[2|6|8|9])+([0-9]{8})|((\+84)+([0-9]{9})))\b/;
        return pattern.test(value) || 'Số điện thoại không đúng.';
    });
    defineRule('onlyCharacters', (value: string) => {
        // Regex pattern allows only letters (both uppercase and lowercase)
        const pattern = /^[A-Za-zÀ-ÖØ-öø-ÿ\sđĐâÂêÊôÔươƯơƠáÁắẮấẤéÉếẾíÍóÓốỐúÚứỨýÝàÀằẰầẦèÈềỀìÌòÒồỒùÙừỪỳỲảẢẳẲẩẨẻẺểỂỉỈỏỎổỔủỦửỬỷỶãÃẵẴẫẪẽẼễỄĩĨõÕỗỖũŨữỮỹỸạẠặẶậẬẹẸệỆịỊọỌộỘụỤựỰỵỴ]+$/;
        return pattern.test(value) || 'Họ tên chỉ được chứa ký tự chữ cái.';
    });

    defineRule('point', (value: string) => {
        const number = parseFloat(value);
        if (isNaN(number) && value != null) {
            return 'Trường này phải là một số.';
        }
        if (number < 0 || number > 100) {
            return 'Số phải nằm trong khoảng từ 0 đến 100.';
        }
        return true;
    });

    
});
