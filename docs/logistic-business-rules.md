# Logistic Automation – İş Tanımı, İş Kuralları ve Fonksiyonel Dokümantasyon

> **Doküman Türü:** Ana İş Dokümantasyonu / Fonksiyonel Gereksinimler / Business Rules  
> **Kapsam:** `logistic-web` ve `logistic-api`  
> **Durum:** Aktif  
> **Kaynak Doküman:** Bu dosya, uygulamanın işleyişi ve iş kuralları için ana referans dokümandır. `Lojistik_Yonetim_Sistemi_Is_Tanimi_ve_Kapsam.md` (iş tanımı ve kapsam) dokümanı bu dosyayla birleştirilmiştir; iş tanımı ve iş kuralları için tek kaynak bu dosyadır.

---

## 1. Dokümanın Amacı

Bu doküman, Logistic Automation projesinin işleyişini, iş kurallarını, modüllerini, operasyonel süreçlerini ve finansal süreçlerini tanımlar.

Proje iki ana uygulamadan oluşmaktadır:

- `logistic-web`
- `logistic-api`

Bu doküman her iki uygulamada yapılacak geliştirmeler için ortak iş referansı olarak kullanılmalıdır.

Yeni bir özellik geliştirilirken, mevcut bir özellik değiştirilirken veya bir iş kuralı güncellenirken yalnızca kod değişikliği yapılmamalıdır.

**İlgili değişiklik bu dokümana da yansıtılmalıdır.**

Bu nedenle bu dosya yaşayan bir dokümandır ve uygulamayla birlikte sürekli güncel tutulmalıdır.

---



## 2. Zorunlu Geliştirme Kuralı

`logistic-web` veya `logistic-api` üzerinde yapılacak her işlevsel geliştirmede aşağıdaki kontrol yapılmalıdır:

1. Değişiklik mevcut bir iş kuralını etkiliyor mu?
2. Yeni bir iş kuralı oluşturuyor mu?
3. Yeni bir ekran, alan, entity, süreç veya durum ekliyor mu?
4. Mevcut bir sürecin davranışını değiştiriyor mu?
5. Finansal hesaplama, operasyon, müşteri, cari, fatura, filo veya raporlama davranışını etkiliyor mu?

Bu sorulardan herhangi birinin cevabı **evet** ise bu Markdown dosyası da aynı geliştirme kapsamında güncellenmelidir.

### Temel Prensip

> **Kod ile dokümantasyon birbiriyle uyumlu olmalıdır.**

Dokümantasyonda bulunmayan bir business rule doğrudan geliştirilmemeli; önce veya geliştirmeyle birlikte bu dosyaya eklenmelidir.

Kod içerisinde bulunan ancak artık geçerli olmayan bir iş kuralı dokümantasyondan kaldırılmalı veya güncellenmelidir.

---



# 3. Sistem Tanımı

Bu sistem; lojistik operasyonlarının, müşteri ilişkilerinin, filo ve araç yönetiminin, yakıt takibinin, gelir-gider işlemlerinin, faturalandırmanın, cari hesapların ve muhasebe süreçlerinin tek bir platform üzerinden yönetilebilmesini sağlayacak kapsamlı bir **Lojistik Yönetim ve İş Takip Sistemi** olacaktır.

Sistemin temel amacı, müşterilerden alınan taşıma işlerinin başlangıcından tahsilat ve ödeme süreçlerinin tamamlanmasına kadar geçen tüm operasyonel ve finansal sürecin uçtan uca takip edilmesidir.

---



# 4. Lojistik Operasyon ve İş Takibi

Müşterilerden gelen yük taşıma talepleri sistem üzerinde bir **iş / operasyon kaydı** olarak oluşturulacaktır.

Taşıma işi mümkün olduğunda şirketin kendi filosundaki araçlarla gerçekleştirilecektir.

Uygun şirket aracı bulunmaması durumunda iş:

- Başka bir lojistik firmasına,
- Taşeron nakliyeciye,
- Kendi aracıyla çalışan bağımsız şoföre

verilebilecektir.

Sistemde müşteri ve nakliyeci kavramları birbirinden tamamen bağımsız düşünülmemelidir.

Bir firma bazı operasyonlarda müşterimiz olabilirken başka bir operasyonda bize taşıma hizmeti sağlayan taşeron veya tedarikçi konumunda olabilir.

Örneğin bir lojistik firması kendi yükünün taşınmasını bizden talep edebilir, başka bir işte ise bizim müşterimizden aldığımız yükü taşıyabilir.

Bu nedenle sistemde firmalar için birden fazla rol tanımlanabilmelidir.

## 4.1. Firma Rolleri

Örnek roller:

- Müşteri
- Nakliyeci
- Taşeron
- Tedarikçi
- Forwarder
- Fabrika
- İthalat / ihracat firması
- Şahıs
- Gümrük firması

Bir firma aynı anda birden fazla role sahip olabilir.

---



# 5. Taşıma Talebi ve Operasyon Bilgileri

Müşteriler taşıma işiyle ilgili bazı bilgileri tarafımıza gönderir.

Eksik olan bilgiler operasyon ekibi tarafından müşteriden talep edilir.

Taşıma kaydı içerisinde mümkün olduğunca aşağıdaki bilgiler tutulacaktır.

## 5.1. Genel İş Bilgileri

- İş tarihi
- Müşteri
- Firma
- Güzergâh
- Yükleme noktası
- Teslimat noktası
- Yükleme tarihi
- Yükleme saati
- Teslimat tarihi
- Referans numarası
- Konşimento numarası
- Açıklama
- Operasyon durumu



## 5.2. Yük Bilgileri

- Yükün nevi / cinsi
- Konteyner numarası
- Mühür numarası
- İthalat / ihracat bilgisi
- Liman bilgisi
- Boş konteyner alım deposu
- Boş konteyner teslim deposu



## 5.3. Araç ve Sürücü Bilgileri

- Çekici plaka
- Dorse plaka
- Sürücü adı
- Sürücü soyadı
- Sürücü telefonu
- Sürücü harcırahı



## 5.4. Müşteriye Verilen Fiyat Bilgileri

- Taşıma fiyatı
- KDV türü
- KDV oranı
- KDV tutarı
- KDV dâhil toplam tutar
- Müşteri komisyonu
- Komisyon oranı / tutarı



## 5.5. Taşeron / Nakliyeci Bilgileri

Eğer taşıma şirketin kendi aracıyla yapılmıyorsa aşağıdaki bilgiler ilgili operasyona bağlanacaktır:

- Nakliyeci / taşeron firma
- Nakliyeci fiyatı
- KDV oranı
- KDV tutarı
- Toplam taşeron maliyeti
- Taşeron fatura tarihi
- Taşeron fatura numarası

---



# 6. Mevcut Excel Alanlarının Sistem Karşılığı

Mevcut Excel takip sisteminde kullanılan bilgiler şunlardır:

- Tarih
- Müşteri
- Firma
- Güzergâh
- Fiyat
- KDV türü
- KDV oranı
- KDV tutarı
- Tutar
- Nakliyeci
- Nakliyeci fiyatı
- Nakliyeci KDV oranı
- Nakliyeci KDV tutarı
- Nakliyeci toplam tutarı
- Yükleme saati
- Sürücü harcırahı
- Referans / konşimento
- Boş konteyner alım / teslim deposu
- Liman
- İthalat / ihracat
- Nevi
- Konteyner no
- Mühür no
- Çekici plaka
- Dorse plaka
- Sürücü isim soyisim
- Telefon
- Müşteri komisyonu
- Komisyon
- Açıklama
- Taşeron fatura tarihi
- Taşeron fatura no

Bu bilgiler yeni sistemde tek bir Excel satırı mantığıyla tutulmamalıdır.

Veriler ilişkisel yapıya ayrılmalıdır.

Örneğin:

- Firma bilgileri → Firma / Customer / Supplier yapısı
- Araç bilgileri → Vehicle
- Dorse bilgileri → Trailer
- Sürücü bilgileri → Driver
- Taşıma işi → Operation / Shipment
- Satış fiyatları → Operation Revenue
- Taşeron maliyetleri → Operation Cost
- Faturalar → Invoice
- Cari hareketler → Account Transaction
- Yakıt → Fuel Record

şeklinde modellenmelidir.

---



# 7. Araç Atama ve Müşteri Bilgilendirme

Operasyon oluşturulduktan sonra taşımanın hangi araçla gerçekleştirileceğine karar verilecektir.

Araç ataması:

- Şirket aracı
- Taşeron firma aracı
- Bağımsız sürücü aracı

şeklinde yapılabilecektir.

Araç ve sürücü belirlendikten sonra müşteriye gönderilecek operasyon bilgileri sistem üzerinden hazırlanabilmelidir.

Örneğin:

- Çekici plakası
- Dorse plakası
- Sürücü adı ve soyadı
- Sürücü telefonu
- Konteyner numarası
- Yükleme tarihi
- Yükleme saati
- Referans numarası

---



# 8. İrsaliye Süreci

Araç yükü almak üzere yola çıktığında veya operasyon başladığında sistemde bulunan taşıma bilgileri kullanılarak gerekli **irsaliye / sevk belgeleri** hazırlanacaktır.

İrsaliye ilgili operasyon kaydıyla ilişkilendirilecektir.

Temel ilişki:

**Operasyon → Araç → Sürücü → İrsaliye → Fatura**

şeklinde takip edilebilmelidir.

---



# 9. Müşteri Faturalandırma Süreci

Yük teslimatı tamamlandıktan sonra müşteriye ilgili taşıma veya hizmet faturası düzenlenecektir.

Fatura içerisinde operasyonla bağlantılı bilgiler yer alabilecektir.

Örneğin:

- Taşıma tarihi
- Güzergâh
- Konteyner numarası
- Araç plakası
- Referans numarası
- Konşimento numarası
- Hizmet açıklaması



## 9.1. Fatura Hizmet Türleri

Fatura türleri yalnızca nakliye hizmetiyle sınırlı değildir.

Sistem aşağıdaki hizmetleri desteklemelidir:

- Nakliye bedeli
- Lojistik hizmet bedeli
- Gümrükleme hizmeti
- Depolama hizmeti
- Liman hizmeti
- Bekleme ücreti
- Ardiye
- Demuraj
- Komisyon
- Ek hizmet bedelleri
- Diğer hizmetler

Her hizmet için farklı KDV oranı veya vergi uygulaması kullanılabilmelidir.

---



# 10. Taşeron Fatura ve Gider Takibi

Bir taşıma işi şirketin kendi aracı yerine başka bir nakliyeci veya taşeron tarafından gerçekleştirildiyse ilgili firmadan gider faturası beklenecektir.

Taşeron faturası geldiğinde aşağıdaki bilgiler sisteme kaydedilecektir:

- İlgili operasyon
- Taşeron firma
- Fatura tarihi
- Fatura numarası
- Matrah
- KDV oranı
- KDV tutarı
- Genel toplam
- Ödeme durumu

Böylece her operasyon için hem müşteriye kesilen gelir faturası hem de taşerondan gelen gider faturası görülebilecektir.

Bu yapı sayesinde operasyon bazında kârlılık hesaplanabilecektir.

Örnek:

**Operasyon Kârı = Müşteriye Kesilen Net Tutar - Taşeron Maliyeti - Operasyona Bağlı Diğer Giderler**

---



# 11. Cari Hesap Yönetimi

Sistemde müşteriler, tedarikçiler ve taşeronlar için cari hesap takibi yapılacaktır.

Müşteriye kesilen her fatura müşterinin cari hesabına otomatik olarak borç kaydı olarak işlenecektir.

Müşteriden ödeme geldiğinde ilgili ödeme cari hesaba kaydedilecektir.

Takip edilmesi gereken başlıca bilgiler:

- Toplam alacak
- Vadesi gelen alacaklar
- Gecikmiş ödemeler
- Yapılmış tahsilatlar
- Açık faturalar
- Kapanmış faturalar
- Cari bakiye

Taşeron veya tedarikçilerden gelen faturalar da ilgili firmanın cari hesabına kaydedilecektir.

Takip edilecek bilgiler:

- Yapılacak ödemeler
- Ödenmiş faturalar
- Vadesi gelen ödemeler
- Gecikmiş ödemeler
- Tedarikçi bakiyesi

---



# 12. Firma ve Muhasebe Kartları

Fatura oluşturulmadan önce müşteri veya satıcı için gerekli muhasebe / firma kaydı oluşturulacaktır.

Firma kartında örneğin:

- Firma adı
- Firma türü
- Vergi dairesi
- Vergi numarası
- T.C. kimlik numarası
- Adres
- Telefon
- E-posta
- Yetkili kişi
- Muhasebe iletişim bilgileri
- IBAN / banka bilgileri
- Varsayılan ödeme vadesi
- Varsayılan KDV bilgileri
- Cari hesap bilgileri

tutulabilecektir.

Bir firma aynı anda birden fazla role sahip olabilir.

---



# 13. Filo Yönetimi

Şirketin sahip olduğu tüm araçlar ve dorseler sisteme kaydedilecektir.

## 13.1. Çekici Bilgileri

- Plaka
- Marka
- Model
- Model yılı
- Şasi numarası
- Kilometre
- Ruhsat bilgileri
- Muayene tarihi
- Sigorta tarihi
- Kasko tarihi
- Araç durumu
- Aktif / pasif durumu



## 13.2. Dorse Bilgileri

- Plaka
- Dorse tipi
- Marka
- Model
- Şasi bilgileri
- Muayene bilgileri
- Aktif / pasif durumu

Taşeronlara ait araçlar da sisteme kaydedilebilmelidir ancak şirket araçlarından ayrı olarak yönetilmelidir.

---



# 14. Sürücü Yönetimi

Şirket sürücüleri ve taşeron sürücüler sistemde tutulacaktır.

Sürücü kartında:

- Ad
- Soyad
- Telefon
- T.C. kimlik numarası
- Ehliyet bilgileri
- SRC bilgileri
- Psikoteknik bilgileri
- Çalıştığı firma
- Kullandığı araç
- Aktif / pasif durumu

gibi bilgiler tutulabilecektir.

Sürücünün geçmişte yaptığı taşıma işleri görüntülenebilmelidir.

---



# 15. Yakıt Takibi

Şirket araçlarının yakıt tüketimleri sistem üzerinden takip edilecektir.

Her yakıt alımında:

- Araç
- Tarih
- Kilometre
- Alınan yakıt miktarı
- Litre fiyatı
- Toplam yakıt maliyeti
- Akaryakıt istasyonu
- Fiş / fatura numarası

kaydedilecektir.

Sistem araç bazında yakıt tüketimini hesaplayabilmelidir.

Örnek raporlar:

- Aylık toplam yakıt
- Toplam yakıt maliyeti
- 100 km ortalama yakıt tüketimi
- Kilometre başına yakıt maliyeti
- Araç bazında yakıt performansı

---



# 16. Araç Giderleri

Yakıt dışında araçlara ait diğer giderler de sisteme kaydedilebilmelidir.

Örneğin:

- Bakım
- Tamir
- Lastik
- Sigorta
- Kasko
- Muayene
- HGS / OGS
- Otoyol
- Köprü
- Park
- Yıkama
- Ceza
- Sürücü harcırahı
- Diğer giderler

Bu giderler mümkün olduğunda ilgili araç ve operasyonlarla ilişkilendirilmelidir.

---



# 17. Gelir-Gider Yönetimi

Sistemde operasyonlardan bağımsız şirket giderleri ve gelirleri de kaydedilebilmelidir.

Gelir ve gider kayıtlarında:

- Tarih
- Firma
- Kategori
- Açıklama
- Tutar
- KDV
- Toplam tutar
- Ödeme şekli
- Banka / kasa
- İlgili operasyon
- İlgili araç

bilgileri tutulabilmelidir.

---



# 18. Operasyon Kârlılık Analizi

Her taşıma operasyonu için gelir ve giderler bir araya getirilerek kârlılık hesaplanacaktır.

Operasyonda:

- Müşteri satış fiyatı
- Taşeron taşıma maliyeti
- Yakıt maliyeti
- Sürücü harcırahı
- Otoyol / köprü maliyetleri
- Diğer operasyon giderleri
- Komisyonlar

dikkate alınacaktır.

Sistem aşağıdaki değerleri hesaplayabilmelidir:

- Brüt kâr
- Net kâr
- Kâr marjı

---



# 19. Müşteri İlişkileri Yönetimi – CRM

Sistem temel müşteri ilişkileri yönetimi özelliklerine sahip olacaktır.

Müşteriler için:

- Yetkili kişiler
- Telefon
- E-posta
- Görüşme notları
- Fiyat teklifleri
- Geçmiş operasyonlar
- Toplam iş hacmi
- Toplam ciro
- Ödeme performansı
- Açık alacak
- Son yapılan iş
- Müşteriye özel fiyatlandırmalar

görülebilecektir.

---



# 20. Teklif ve Fiyat Yönetimi

Müşteriye taşıma öncesinde fiyat teklifi verilebilmelidir.

Teklif içerisinde:

- Müşteri
- Güzergâh
- Taşıma tipi
- Araç tipi
- Konteyner tipi
- Hizmet
- Fiyat
- KDV
- Geçerlilik tarihi
- Ödeme koşulları

yer alabilecektir.

Müşteri teklifi kabul ettiğinde teklif doğrudan operasyona dönüştürülebilmelidir.

---



# 21. Operasyon Durumları

Her taşıma işinin süreç içerisindeki durumu takip edilmelidir.

Örnek durumlar:

1. Talep alındı
2. Fiyat bekleniyor
3. Teklif verildi
4. Müşteri onayı bekleniyor
5. Onaylandı
6. Araç aranıyor
7. Araç atandı
8. Yükleme bekleniyor
9. Yüklendi
10. Yolda
11. Teslim edildi
12. Evrak bekleniyor
13. Faturalandırılacak
14. Faturalandırıldı
15. Ödeme bekleniyor
16. Tamamlandı
17. İptal edildi

Durum geçişleri business rule olarak tanımlanmalıdır.

Her durumdan her duruma doğrudan geçiş yapılmasına izin verilmemelidir.

---



# 22. Dashboard ve Raporlama

Sistem yönetim ekranında şirketin genel durumunu gösterecek dashboard bulunmalıdır.

Dashboard üzerinde örneğin:

- Bugünkü operasyonlar
- Devam eden operasyonlar
- Bekleyen yüklemeler
- Teslim edilen yükler
- Faturası kesilmeyen işler
- Taşeron faturası gelmeyen işler
- Tahsilatı bekleyen faturalar
- Ödemesi yaklaşan faturalar
- Toplam aylık ciro
- Toplam aylık gider
- Brüt kâr
- Net kâr
- Araç doluluk oranı
- Araç kullanım oranı
- Yakıt tüketimi
- En çok iş yapılan müşteriler
- En çok kullanılan taşeronlar

görülebilmelidir.

---



# 23. Sistem Ana Modülleri

Sistem genel olarak aşağıdaki modüllerden oluşacaktır:

1. Dashboard
2. Operasyon / İş Takibi
3. Müşteri Yönetimi
4. Firma / Tedarikçi / Taşeron Yönetimi
5. CRM
6. Teklif Yönetimi
7. Filo Yönetimi
8. Dorse Yönetimi
9. Sürücü Yönetimi
10. Yakıt Yönetimi
11. Araç Gider Yönetimi
12. İrsaliye Yönetimi
13. Satış Faturaları
14. Alış / Gider Faturaları
15. Cari Hesap Yönetimi
16. Tahsilat Yönetimi
17. Ödeme Yönetimi
18. Gelir-Gider Yönetimi
19. Muhasebe Raporları
20. Operasyon Kârlılık Raporları
21. Filo Performans Raporları
22. Müşteri Analizleri
23. Kullanıcı ve Yetkilendirme Yönetimi

---



# 24. Temel İş Akışı

Sistemin ana operasyon süreci aşağıdaki şekilde olacaktır:

**Müşteriden taşıma talebi alınması**

→ Müşteri / firma bilgilerinin kontrol edilmesi

→ Güzergâh ve yük bilgilerinin girilmesi

→ Müşteriye fiyat verilmesi

→ Teklifin müşteri tarafından onaylanması

→ Şirket filosundaki uygun araçların kontrol edilmesi

→ Uygun araç varsa şirket aracının atanması

→ Uygun araç yoksa taşeron / nakliyeci bulunması

→ Araç ve sürücünün operasyona atanması

→ Araç bilgilerinin müşteriye bildirilmesi

→ İrsaliyenin hazırlanması

→ Yükleme yapılması

→ Taşımanın gerçekleştirilmesi

→ Teslimatın tamamlanması

→ Operasyon evraklarının sisteme eklenmesi

→ Müşteri faturasının hazırlanması

→ Faturanın müşteriye gönderilmesi

→ Faturanın müşteri carisine işlenmesi

→ Taşeron kullanıldıysa taşeron faturasının beklenmesi

→ Taşeron faturasının ilgili operasyona bağlanması

→ Taşeron borcunun cari hesaba işlenmesi

→ Müşteri tahsilatının takip edilmesi

→ Taşeron ödemesinin takip edilmesi

→ Operasyon gelir-giderlerinin hesaplanması

→ Operasyon kârlılığının hesaplanması

→ Operasyonun finansal olarak kapatılması

---



# 25. Sistemin Temel Veri İlişkisi

Mevcut Excel yaklaşımında operasyon, araç, müşteri, fatura ve finans bilgileri tek bir satır üzerinde, birbirinden bağımsız kolonlarda tutulmaktadır.

Yeni sistemde tüm kayıtlar ilişkisel hale getirilecektir.

Bir operasyon için temel ilişki aşağıdaki şekilde düşünülmelidir:

**Müşteri**  
**→ Operasyon**  
**→ Yük**  
**→ Güzergâh**  
**→ Araç / Taşeron**  
**→ Sürücü**  
**→ Dorse**  
**→ İrsaliye**  
**→ Satış Faturası**  
**→ Gider Faturası**  
**→ Tahsilat**  
**→ Ödeme**  
**→ Operasyon Giderleri**  
**→ Kârlılık**

Bir operasyon açıldığında yukarıdaki ilişki zincirinin tamamı kullanıcıya **tek ekran üzerinden** uçtan uca takip ettirilebilmelidir.

Bu ilişkiler hem `logistic-api` domain modelinde hem de `logistic-web` kullanıcı deneyiminde korunmalıdır.

---



# 26. Logistic API İçin Temel İş Kuralları

`logistic-api`, sistemin business rule ve domain davranışlarının ana sahibi olmalıdır.

Frontend üzerinde kritik iş kuralları uygulanarak güvenlik sağlanmaya çalışılmamalıdır.

`logistic-api` aşağıdaki sorumluluklara sahip olacaktır:

- Operasyon oluşturma ve güncelleme kuralları
- Firma rol yönetimi
- Araç uygunluk kontrolleri
- Taşeron atama kuralları
- Operasyon durum geçişleri
- Fatura / operasyon ilişkileri
- Cari hareketlerin oluşturulması
- Tahsilat ve ödeme eşleştirmeleri
- Gelir / gider hesaplamaları
- KDV hesaplamaları
- Operasyon kârlılığı
- Yakıt tüketim hesaplamaları
- Filo gider hesaplamaları
- Veri bütünlüğü
- Yetkilendirme kontrolleri

Business rule yalnızca frontend tarafında bulunmamalıdır.

---



# 27. Logistic Web İçin Temel Kurallar

`logistic-web`, kullanıcı ile sistem arasındaki yönetim arayüzüdür.

Temel sorumlulukları:

- Kullanıcıdan veri almak
- Gerekli doğrulamaları göstermek
- `logistic-api` ile haberleşmek
- Operasyon süreçlerini kullanıcıya anlaşılır şekilde sunmak
- Dashboard ve raporları göstermek
- İşlemlerin durumlarını kullanıcıya göstermek
- Hata mesajlarını doğru şekilde sunmak

Frontend API'nin business rule'larını tekrar eden bağımsız bir domain katmanı haline getirilmemelidir.

Frontend validation kullanıcı deneyimini iyileştirmek amacıyla kullanılabilir ancak nihai doğrulama backend tarafından yapılmalıdır.

---



# 28. Mimari Prensipler

Her iki proje geliştirilirken aşağıdaki prensiplere uyulmalıdır:

- SOLID
- Clean Code
- Separation of Concerns
- Dependency Inversion
- DRY
- KISS
- Explicit Business Rules
- Modüler mimari
- Test edilebilir yapı
- Düşük bağımlılık
- Yüksek cohesion

Domain kavramları rastgele isimlendirilmemelidir.

Aynı kavram frontend, backend ve dokümantasyonda mümkün olduğunca aynı terminolojiyle ifade edilmelidir.

---



# 29. Dokümantasyon Güncelleme Politikası

Bu dosya uygulamanın yaşayan iş dokümantasyonudur.

Aşağıdaki değişiklikler yapıldığında doküman güncellenmelidir:

- Yeni modül
- Yeni ekran
- Yeni operasyon alanı
- Yeni firma tipi
- Yeni araç tipi
- Yeni fatura türü
- Yeni cari hareket türü
- Yeni operasyon durumu
- Yeni iş akışı
- Yeni finansal hesaplama
- Yeni rapor
- Yeni kullanıcı rolü
- Yeni yetki
- Business rule değişikliği
- Mevcut alan veya süreç kaldırılması
- Veri modelinde iş anlamı taşıyan değişiklik

---



# 30. Değişiklik Geçmişi

Bu bölüm her önemli fonksiyonel değişiklikte güncellenmelidir.


| Tarih      | Versiyon | Değişiklik    | Açıklama                                                                                        |
| ---------- | -------- | ------------- | ----------------------------------------------------------------------------------------------- |
| 2026-08-31 | 1.0.0    | İlk oluşturma | Logistic Automation iş tanımı, modüller, temel iş akışları ve geliştirme kuralları oluşturuldu. |
| 2026-10-17 | 1.1.0    | Doküman birleştirme | `Lojistik_Yonetim_Sistemi_Is_Tanimi_ve_Kapsam.md` içeriği bu dosyayla birleştirildi; cari hesap otomatik borç kaydı ve tek ekran takip gereksinimi eklendi. |


---



# 31. Definition of Done – Dokümantasyon Kontrolü

Bir feature tamamlanmış kabul edilmeden önce aşağıdaki kontrol yapılmalıdır:

- [ ] Business requirement karşılandı.
- [ ] API business rule doğru yerde uygulanıyor.
- [ ] Frontend gerekli kullanıcı akışını destekliyor.
- [ ] Veri modeli mevcut domain yapısıyla uyumlu.
- [ ] Validasyonlar tanımlandı.
- [ ] Hata senaryoları ele alındı.
- [ ] Yetkilendirme kontrol edildi.
- [ ] Testler eklendi / güncellendi.
- [ ] Bu dokümanın etkilenip etkilenmediği kontrol edildi.
- [ ] Etkileniyorsa doküman güncellendi.
- [ ] Değişiklik geçmişine önemli fonksiyonel değişiklik eklendi.

---



# 32. Nihai Sistem Hedefi

Sistem yalnızca bir lojistik taşıma kayıt programı değildir.

Amaç;

**Lojistik Operasyon  

- CRM  
- Filo Yönetimi  
- Araç / Dorse Yönetimi  
- Yakıt Yönetimi  
- Gelir-Gider Yönetimi  
- Cari Hesap  
- Faturalandırma  
- Ön Muhasebe  
- Tahsilat / Ödeme Takibi  
- Operasyon Kârlılığı  
- Raporlama**

fonksiyonlarını tek bir sistem altında birleştiren kapsamlı bir **Lojistik ERP / Yönetim Sistemi** oluşturmaktır.

---



# 33. Ana Kural

> `logistic-web` veya `logistic-api` üzerinde yapılan bir değişiklik sistemin işleyişini etkiliyorsa, bu doküman da aynı değişiklik kapsamında güncellenmelidir.

> Bu dosya projenin iş kuralları açısından **Single Source of Truth** olarak kabul edilmelidir.

