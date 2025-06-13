<template>
  <div class="w-md border border-black border-solid p-4" style="margin: 50px auto;">
    <a-form
      ref="formRef"
      name="product_form"
      :model="formState"
      @finish="onFinish"
    >
      <!-- Nume produs -->
      <a-form-item
        label="Product Name"
        name="name"
        :rules="[{ required: true, message: 'Please input the product name!' }]"
      >
        <a-input v-model:value="formState.name" />
      </a-form-item>

      <!-- Pret produs -->
      <a-form-item
        label="Price"
        name="price"
        :rules="[{ required: true, message: 'Please input the price!' }]"
      >
        <a-input-number v-model:value="formState.price" :min="0" style="width: 100%;" />
      </a-form-item>

      <!-- Atribute   -->
      <a-space
        v-for="(attr, index) in formState.attributes"
        :key="index"
        style="display: flex; margin-bottom: 8px"
        align="baseline"
      >
        <a-form-item
          :name="['attributes', index, 'key']"
          :rules="[{ required: true, message: 'Missing attribute key' }]"
        >
          <a-input v-model:value="attr.key" placeholder="Attribute name (e.g., Color)" />
        </a-form-item>

        <a-form-item
          :name="['attributes', index, 'value']"
          :rules="[{ required: true, message: 'Missing attribute value' }]"
        >
          <a-input v-model:value="attr.value" placeholder="Attribute value (e.g., Red)" />
        </a-form-item>

        <MinusCircleOutlined @click="removeAttribute(attr)" />
      </a-space>

      <a-form-item>
        <a-button type="dashed" block @click="addAttribute">
          <PlusOutlined />
          Add Attribute
        </a-button>
      </a-form-item>
      <!-- main image -->

       <a-form-item label="Main Image" name="main image">
        <a-upload
          v-model:file-list="mainImage"
          list-type="picture-card"
         :before-upload="handleBeforeUploadMain"
          @preview="handlePreview"
          :max-count="8"
        >
          <div v-if="mainImage.length < 8">
            <plus-outlined />
            <div style="margin-top: 8px">Upload</div>
          </div>
        </a-upload>

        <a-modal :open="previewVisible" :title="previewTitle" :footer="null" @cancel="handleCancel">
          <img alt="example" style="width: 100%" :src="previewImage" />
        </a-modal>
      </a-form-item>


      <!-- images -->
      <a-form-item label="Images" name="images">
        <a-upload
          v-model:file-list="fileList"
          list-type="picture-card"
         :before-upload="handleBeforeUpload"
          @preview="handlePreview"
          :max-count="8"
        >
          <div v-if="fileList.length < 8">
            <plus-outlined />
            <div style="margin-top: 8px">Upload</div>
          </div>
        </a-upload>

        <a-modal :open="previewVisible" :title="previewTitle" :footer="null" @cancel="handleCancel">
          <img alt="example" style="width: 100%" :src="previewImage" />
        </a-modal>
      </a-form-item>
      <!-- Submit -->
      <a-form-item>
        <a-button type="primary" html-type="submit">Save Product</a-button>
      </a-form-item>
    </a-form>
  </div>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons-vue'
import type { FormInstance, UploadFile } from 'ant-design-vue'
 
 


interface Attribute {
  key: string
  value: string
}

const formRef = ref<FormInstance>()

const formState = reactive({
  name: '',
  price: null,
  description: '',
  availableStock: 0,
  categoryId: 0,
  attributes: [] as Attribute[],
})

const addAttribute = () => {
  formState.attributes.push({
    key: '',
    value: '',
  })
}

const removeAttribute = (attr: Attribute) => {
  const index = formState.attributes.indexOf(attr)
  if (index !== -1) formState.attributes.splice(index, 1)
}

const mainImage = ref([]);
const fileList = ref([])

const previewVisible = ref(false)
const previewImage = ref('')
const previewTitle = ref('')

const config = useRuntimeConfig();

const handleCancel = () => {
  previewVisible.value = false
  previewTitle.value = ''
}

function getBase64(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.readAsDataURL(file)
    reader.onload = () => resolve(reader.result)
    reader.onerror = error => reject(error)
  })
}


const handlePreview = async (file) => {
  if (!file.url && !file.preview) {
    file.preview = await getBase64(file.originFileObj)
  }
  previewImage.value = file.url || file.preview
  previewVisible.value = true
  previewTitle.value = file.name || file.url.substring(file.url.lastIndexOf('/') + 1)
}


const handleBeforeUpload = async (file) => {
  const formData = new FormData()
  formData.append('file', file)

  try {
    const response = await fetch(config.public.uploadImageUrl, {
      method: 'POST',
      body: formData
    })
    const data = await response.json()
    file.url = data.url  
    file.response = { url: data.url }
    file.status = 'done'
    fileList.value.push(file)
  } catch (err) {
    file.status = 'error'
    console.error('Upload error:', err)
  }

  return false // prevenim uploadul automat
}

const handleBeforeUploadMain = async (file) => {
  const formData = new FormData()
  formData.append('file', file)

  try {
    const response = await fetch(config.public.uploadImageUrl, {
      method: 'POST',
      body: formData
    })
    const data = await response.json()
    file.url = data.url  
    file.response = { url: data.url }
    file.status = 'done'
    mainImage.value.push(file)
  } catch (err) {
    file.status = 'error'
    console.error('Upload error:', err)
  }

  return false // prevenim uploadul automat
}

const onFinish = async () => {
  const imageUrls = fileList.value
    .filter(file  => file.status === 'done')
    .map(file => file.response?.url)
    .filter(Boolean)

    const mainImageUrls = mainImage.value
    .filter(file => file.status === 'done')
    .map(file => file.response?.url)
    .filter(Boolean)

  const payload = {
    ...formState,
    attributes: Object.fromEntries(formState.attributes.map(a => [a.key, a.value])),
    images: imageUrls,
    image: mainImageUrls[0] || ""
  }

  try {
    const res = await fetch(config.public.productsUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload)
    })

    if (res.ok) {
      alert('Produs salvat cu succes')
    } else {
      alert('Eroare la salvare:' + await res.text())
    }
  } catch (err) {
    alert('Eroare la salvare:' + err)
  }
}
 

 
</script>
