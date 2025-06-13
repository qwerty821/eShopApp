<template>
  <div class="w-full max-w-sm p-4 bg-white border border-gray-200 rounded-lg shadow-sm sm:p-6 md:p-8 dark:bg-gray-800 dark:border-gray-700" style="margin: 100px auto;">
    <div class="flex flex-col items-center justify-center">
      <img src="" alt="aaaaa"/>
      <p class="text-2xl">Log In</p>
    </div>
    <a-form
      ref="formRef"
      :model="formState"
      name="normal_login"
      class="login-form"
      @finish="onFinish"
      @finishFailed="onFinishFailed"
    >
      <a-form-item
        label="Username"
        name="username"
        :rules="[
          { required: true, message: 'Please input your username!' },
          { min: 4, message: 'Username must be at least 4 characters!' }
        ]"
      >
        <a-input v-model:value="formState.username">
          <template #prefix>
            <UserOutlined class="site-form-item-icon" />
          </template>
        </a-input>
      </a-form-item>

      <a-form-item
        label="Password"
        name="password"
        :rules="[
          { required: true, message: 'Please input your password!' },
          { min: 6, message: 'Password must be at least 6 characters!' }
        ]"
      >
        <a-input-password v-model:value="formState.password">
          <template #prefix>
            <LockOutlined class="site-form-item-icon" />
          </template>
        </a-input-password>
      </a-form-item>

      <a-form-item>
        <a-form-item name="remember" no-style>
          <a-checkbox v-model:checked="formState.remember">Remember me</a-checkbox>
        </a-form-item>
        <a class="login-form-forgot" href="">Forgot password</a>
      </a-form-item>

      <a-form-item>
        <a-button :disabled="disabled" type="primary" html-type="submit" class="login-form-button">
          Log in
        </a-button>
        Or
        <a href="">register now!</a>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { UserOutlined, LockOutlined } from '@ant-design/icons-vue';

interface FormState {
  username: string;
  password: string;
  remember: boolean;
}
const formState = ref<FormState>({
  username: '',
  password: '',
  remember: true,
});

const formRef = ref();

const router = useRouter()

const onFinish = async (values: any) => {
	const user = useUserStore();
	const res = await user.login(values as IUserLoginDTO);
	
	if (res.success) {
		router.push("/");
	} else {
		alert(res.message);
	}
 
};

const onFinishFailed = (errorInfo: any) => {
  console.log('Failed:', errorInfo);
};

const disabled = computed(() => {
  return !(formState.value.username && formState.value.password);
});
</script>


<style scoped>
#components-form-demo-normal-login .login-form {
	max-width: 300px;
}

#components-form-demo-normal-login .login-form-forgot {
	float: right;
}

#components-form-demo-normal-login .login-form-button {
	width: 100%;
}
</style>