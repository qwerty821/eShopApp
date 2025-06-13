<template>
  <section class="body-font overflow-hidden">
    <div class="container px-5 py-24 mx-auto">
      <div class="grid grid-cols-1 lg:grid-cols-[4fr_5fr_2fr] gap-5 items-start">

        <!-- COL 1: Imagine produs -->
        <div>
          <!-- <a-image :preview="{ visible: false }" :src=item?.image @click="visible = true" /> -->
          <div style="display: none">
            <a-image-preview-group v-if="imagesList != null"
              :preview="{ visible, onVisibleChange: (vis: any) => (visible = vis), current: currentImageIndex }">
              <a-image :src="imagesList[0]" />
              <a-image v-for="it in imagesList.length" :src="imagesList[it - 1]" :key=it />
            </a-image-preview-group>
          </div>
          <!--  -->
          <Carousel v-if="imagesList != null" id="gallery" v-bind="galleryConfig" v-model="currentSlide">
            <Slide v-for="it in imagesList.length" :key="it">
              <img :preview="{ visible: false }" :src="imagesList[it - 1]" alt="Gallery Image" class="gallery-image"
                @click="changeVis(it)" />
            </Slide>
          </Carousel>

          <Carousel v-if="imagesList != null" id="thumbnails" v-bind="thumbnailsConfig" v-model="currentSlide">
            <Slide v-for="it in imagesList.length" :key="it">
              <template #default="{ currentIndex, isActive }">
                <div :class="['thumbnail', { 'is-active': isActive }]" @click="slideTo(currentIndex)">
                  <img :src="imagesList[it - 1]" alt="Thumbnail Image" class="thumbnail-image" />
                </div>
              </template>
            </Slide>

            <template #addons>
              <Navigation />
            </template>
          </Carousel>
          <!--  -->
        </div>

        <!-- COL 2: Detalii produs -->
        <div class="space-y-4">
          <h2 class="text-sm text-gray-500 tracking-widest">BRAND NAME</h2>
          <h1 class="text-3xl font-semibold text-gray-900">{{ item?.name }}</h1>

          <div class="flex items-center space-x-4">
            <div class="flex text-blue-500">
              <span v-for="i in 4" :key="i">★</span>
              <span class="text-gray-400">☆</span>
            </div>
            <span class="text-sm text-gray-600">4 Reviews</span>
          </div>

          <p class="text-xl font-bold text-gray-900" style="color: #ff1d25;">{{ item?.price }} Lei</p>

          <!-- Specificatii -->
          <div>
            <h3 class="text-lg font-semibold mb-2">Specificații</h3>
            <div class="grid grid-cols-2 gap-2">
              <div v-for="(value, key) in item?.attributes" :key="key" class="flex justify-between col-span-2">
                <span class="font-bold " style="font-weight: bold !important; ">{{ key }}</span>
                <span class="">{{ value }}</span>
              </div>
            </div>
          </div>

          <p class="text-gray-700 pt-4 border-t">{{ item?.description }}</p>
          <p v-for="i in 10" class="text-gray-700 pt-4 border-t">Lorem ipsum dolor, sit amet consectetur adipisicing
            elit.
            Explicabo neque aut iure iusto natus error libero ab ducimus, quos corrupti! Praesentium, illum voluptatem
            sint esse
            est quis labore minus eveniet! Lorem ipsum dolor sit amet consectetur adipisicing elit. Saepe tenetur
            architecto
            aperiam aut asperiores dolorum totam voluptatem obcaecati illum cum aliquam, libero nostrum unde neque error
            reiciendis quis ipsum praesentium?</p>
        </div>

        <!-- COL 3: Card cumparare -->
        <div
          class="sticky top-24 self-start w-full max-w-xs bg-white border rounded-2xl shadow-lg p-6 flex flex-col gap-4">
          <div class="text-3xl font-bold text-red-600">{{ item?.price }} Lei</div>

          <div class="text-sm font-medium"
            :class="Number(item?.availableStock) > 0 ? 'text-green-600' : 'text-red-500'">
            {{ Number(item?.availableStock) > 0
              ? `În stoc: ${Number(item?.availableStock)} bucăți`
              : 'Indisponibil' }}
          </div>

          <button class="w-full py-2 px-4 bg-blue-600 text-white font-semibold rounded-lg hover:bg-blue-700 transition">
            🛒 Adaugă în coș
          </button>

          <button
            class="w-full py-2 px-4 border border-gray-300 text-gray-700 font-semibold rounded-lg hover:bg-gray-100 transition">
            🛍️ Cumpără acum
          </button>

          <p class="text-sm text-gray-500 text-center">
            Livrare estimată: 2–3 zile lucrătoare
          </p>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts" setup>

definePageMeta({
  middleware: ['auth']
})

const route = useRoute()
const { slug } = route.params;

const { data: product, pending, error } = await useGetProduct(slug.toString());
const item = computed(() => product.value as IProduct | null);

const imagesList: string[] = [];
if (item.value?.image != null) {
  imagesList.push(item.value.image);
}
if (item.value?.images != null) {
  imagesList.push(...item.value.images);
}


const attr = computed(() => {
  if (!item.value?.attributes) return []
  return Object.entries(item.value.attributes)
})

// pentru imagini

import { ref } from 'vue';
const visible = ref(false);

const currentImageIndex = ref(0)

function changeVis(index: number) {
  currentImageIndex.value = index
  visible.value = true
}

import 'vue3-carousel/carousel.css'
import { Carousel, Slide, Navigation } from 'vue3-carousel'

const currentSlide = ref(0)

const slideTo = (nextSlide : any) => (currentSlide.value = nextSlide)

const galleryConfig = {
  itemsToShow: 1,
  wrapAround: true,
  slideEffect: "fade" as "fade",
  mouseDrag: false,
  touchDrag: false,
  height: 320,
}

const thumbnailsConfig = {
  height: 80,
  itemsToShow: 6,
  wrapAround: false,
  touchDrag: false,
  gap: 10,
}

</script>

<style scoped>
:root {
  background-color: #242424;
}

.carousel {
  --vc-nav-background: rgba(255, 255, 255, 0.7);
  --vc-nav-border-radius: 100%;
}

img {
  border-radius: 8px;
  width: 100%;
  height: 100%;
  object-fit: contain;
}

.gallery-image {
  border-radius: 16px;
}

#thumbnails {
  margin-top: 10px;
}

.thumbnail {
  height: 100%;
  width: 100%;
  cursor: pointer;
  opacity: 0.6;
  transition: opacity 0.3s ease-in-out;
}

.thumbnail.is-active,
.thumbnail:hover {
  opacity: 1;
}
</style>