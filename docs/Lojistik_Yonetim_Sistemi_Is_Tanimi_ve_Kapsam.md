# Lojistik Yönetim Sistemi – İş Tanımı ve Kapsam

Bu sistem; lojistik operasyonlarının, müşteri ilişkilerinin, filo ve araç yönetiminin, yakıt takibinin, gelir-gider işlemlerinin, faturalandırmanın, cari hesapların ve muhasebe süreçlerinin tek bir platform üzerinden yönetilebilmesini sağlayacak kapsamlı bir Lojistik Yönetim ve İş Takip Sistemi olacaktır.

Sistemin temel amacı, müşterilerden alınan taşıma işlerinin başlangıcından tahsilat ve ödeme süreçlerinin tamamlanmasına kadar geçen tüm operasyonel ve finansal sürecin uçtan uca takip edilmesidir.

## 1. Lojistik Operasyon ve İş Takibi

Müşterilerden gelen yük taşıma talepleri sistem üzerinde bir iş / operasyon kaydı olarak oluşturulacaktır.

Taşıma işi mümkün olduğunda şirketin kendi filosundaki araçlarla gerçekleştirilecektir. Uygun şirket aracı bulunmaması durumunda iş;

- Başka bir lojistik firmasına,
- Taşeron nakliyeciye,
- Kendi aracıyla çalışan bağımsız şoföre

verilebilecektir.

Sistemde müşteri ve nakliyeci kavramları birbirinden tamamen bağımsız düşünülmemelidir. Bir firma bazı operasyonlarda müşterimiz olabilirken başka bir operasyonda bize taşıma hizmeti sağlayan taşeron veya tedarikçi konumunda olabilir.

Örneğin bir lojistik firması kendi yükünün taşınmasını bizden talep edebilir, başka bir işte ise bizim müşterimizden aldığımız yükü taşıyabilir.

Bu nedenle sistemde firmalar için birden fazla rol tanımlanabilmelidir.

Firma rolleri örneğin:

- Müşteri
- Nakliyeci
- Taşeron
- Tedarikçi
- Forwarder
- Fabrika
- İthalat / ihracat firması
- Şahıs
- Gümrük firması

şeklinde oluşturulabilmelidir.

## 2. Taşıma Talebi ve Operasyon Bilgileri

Müşteriler taşıma işiyle ilgili bazı bilgileri tarafımıza gönderir. Eksik olan bilgiler operasyon ekibi tarafından müşteriden talep edilir.

Taşıma kaydı içerisinde mümkün olduğunca aşağıdaki bilgiler tutulacaktır:

### Genel İş Bilgileri

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

### Yük Bilgileri

- Yükün nevi / cinsi
- Konteyner numarası
- Mühür numarası
- İthalat / ihracat bilgisi
- Liman bilgisi
- Boş konteyner alım deposu
- Boş konteyner teslim depose

### Araç ve Sürücü Bilgileri

- Çekici plaka
- Dorse plaka
- Sürücü adı
- Sürücü soyadı
- Sürücü telefonu
- Sürücü harcırahı

### Müşteriye Verilen Fiyat Bilgileri

- Taşıma fiyatı
- KDV türü
- KDV oranı
- KDV tutarı
- KDV dâhil toplam tutar
- Müşteri komisyonu
- Komisyon oranı / tutarı

### Taşeron / Nakliyeci Bilgileri

Eğer taşıma şirketin kendi aracıyla yapılmıyorsa;

- Nakliyeci / taşeron firma
- Nakliyeci fiyatı
- KDV oranı
- KDV tutarı
- Toplam taşeron maliyeti
- Taşeron fatura tarihi
- Taşeron fatura numarası

bilgileri ilgili operasyona bağlanacaktır.

## 3. Araç Atama ve Müşteri Bilgilendirme

Operasyon oluşturulduktan sonra taşımanın hangi araçla gerçekleştirileceğine karar verilecektir.

Araç ataması;

- Şirket aracı,
- Taşeron firma aracı,
- Bağımsız sürücü aracı

şeklinde yapılabilecektir.

Araç ve sürücü belirlendikten sonra müşteriye gönderilecek operasyon bilgileri sistem üzerinden hazırlanabilmelidir.

Bu bilgiler örneğin;

- Çekici plakası
- Dorse plakası
- Sürücü adı ve soyadı
- Sürücü telefonu
- Konteyner numarası
- Yükleme tarihi
- Yükleme saati
- Referans numarası

gibi bilgileri içerebilir.

## 4. İrsaliye Süreci

Araç yükü almak üzere yola çıktığında veya operasyon başladığında sistemde bulunan taşıma bilgileri kullanılarak gerekli irsaliye / sevk belgeleri hazırlanacaktır.

İrsaliye ilgili operasyon kaydıyla ilişkilendirilecektir.

Böylece bir taşıma işi için;

Operasyon → Araç → Sürücü → İrsaliye → Fatura

ilişkisi sistem üzerinden izlenebilecektir.

## 5. Müşteri Faturalandırma Süreci

Yük teslimatı tamamlandıktan sonra müşteriye ilgili taşıma veya hizmet faturası düzenlenecektir.

Fatura içerisinde operasyonla bağlantılı bazı bilgiler yer alabilecektir.

Örneğin:

- Taşıma tarihi
- Güzergâh
- Konteyner numarası
- Araç plakası
- Referans numarası
- Konşimento numarası
- Hizmet açıklaması

Fatura türleri sadece nakliye hizmetiyle sınırlı olmayacaktır.

Sistem aşağıdaki gibi farklı hizmet türlerini desteklemelidir:

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

## 6. Taşeron Fatura ve Gider Takibi

Bir taşıma işi şirketin kendi aracı yerine başka bir nakliyeci veya taşeron tarafından gerçekleştirildiyse ilgili firmadan gider faturası beklenecektir.

Taşeron faturası geldiğinde;

- İlgili operasyon
- Taşeron firma
- Fatura tarihi
- Fatura numarası
- Matrah
- KDV oranı
- KDV tutarı
- Genel toplam
- Ödeme durumu

bilgileri sisteme kaydedilecektir.

Böylece her operasyon için hem müşteriye kesilen gelir faturası hem de taşerondan gelen gider faturası görülebilecektir.

Bu yapı sayesinde operasyon bazında kârlılık hesaplanabilecektir.

Örneğin:

**Operasyon Kârı = Müşteriye Kesilen Net Tutar - Taşeron Maliyeti - Operasyona Bağlı Diğer Giderler**

## 7. Cari Hesap Yönetimi

Sistemde müşteriler, tedarikçiler ve taşeronlar için cari hesap takibi yapılacaktır.

Müşteriye kesilen her fatura müşterinin cari hesabına otomatik olarak borç kaydı olarak işlenecektir.

Müşteriden ödeme geldiğinde ilgili ödeme cari hesaba kaydedilecektir.

Bu sayede;

- Toplam alacak
- Vadesi gelen alacaklar
- Gecikmiş ödemeler
- Yapılmış tahsilatlar
- Açık faturalar
- Kapanmış faturalar
- Cari bakiye

takip edilebilecektir.

Aynı şekilde taşeron veya tedarikçilerden gelen faturalar da ilgili firmanın cari hesabına kaydedilecektir.

Buradan;

- Yapılacak ödemeler
- Ödenmiş faturalar
- Vadesi gelen ödemeler
- Gecikmiş ödemeler
- Tedarikçi bakiyesi

takip edilebilecektir.

## 8. Firma ve Muhasebe Kartları

Fatura oluşturulmadan önce müşteri veya satıcı için gerekli muhasebe / firma kaydı oluşturulacaktır.

Firma kartında örneğin;

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

Bir firma aynı anda birden fazla role sahip olabilecektir.

## 9. Filo Yönetimi

Şirketin sahip olduğu tüm araçlar ve dorseler sisteme kaydedilecektir.

### Çekici Bilgileri

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

### Dorse Bilgileri

- Plaka
- Dorse tipi
- Marka
- Model
- Şasi bilgileri
- Muayene bilgileri
- Aktif / pasif durumu

Taşeronlara ait araçlar da sisteme kaydedilebilmelidir ancak şirket araçlarından ayrı olarak yönetilmelidir.

## 10. Sürücü Yönetimi

Şirket sürücüleri ve taşeron sürücüler sistemde tutulacaktır.

Sürücü kartında;

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

Sürücünün geçmişte yaptığı taşıma işleri de görüntülenebilmelidir.

## 11. Yakıt Takibi

Şirket araçlarının yakıt tüketimleri sistem üzerinden takip edilecektir.

Her yakıt alımında;

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

Örneğin;

- Aylık toplam yakıt
- Toplam yakıt maliyeti
- 100 km ortalama yakıt tüketimi
- Kilometre başına yakıt maliyeti
- Araç bazında yakıt performansı

raporlanabilmelidir.

## 12. Araç Giderleri

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

Bu giderler mümkünse ilgili araç ve operasyonlarla ilişkilendirilmelidir.

## 13. Gelir-Gider Yönetimi

Sistemde operasyonlardan bağımsız şirket giderleri ve gelirleri de kaydedilebilmelidir.

Gelir ve gider kayıtlarında;

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

## 14. Operasyon Kârlılık Analizi

Her taşıma operasyonu için gelir ve giderler bir araya getirilerek kârlılık hesaplanacaktır.

Operasyonda;

- Müşteri satış fiyatı
- Taşeron taşıma maliyeti
- Yakıt maliyeti
- Sürücü harcırahı
- Otoyol / köprü maliyetleri
- Diğer operasyon giderleri
- Komisyonlar

dikkate alınacaktır.

Sistem;

- Brüt kâr
- Net kâr
- Kâr marjı

hesaplayabilmelidir.

## 15. Müşteri İlişkileri Yönetimi – CRM

Sistem aynı zamanda temel müşteri ilişkileri yönetimi özelliklerine sahip olacaktır.

Müşteriler için;

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

## 16. Teklif ve Fiyat Yönetimi

Müşteriye taşıma öncesinde fiyat teklifi verilebilmelidir.

Teklif içerisinde;

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

## 17. Operasyon Durumları

Her taşıma işinin süreç içerisindeki durumu takip edilmelidir.

Örnek operasyon durumları:

- Talep alındı
- Fiyat bekleniyor
- Teklif verildi
- Müşteri onayı bekleniyor
- Onaylandı
- Araç aranıyor
- Araç atandı
- Yükleme bekleniyor
- Yüklendi
- Yolda
- Teslim edildi
- Evrak bekleniyor
- Faturalandırılacak
- Faturalandırıldı
- Ödeme bekleniyor
- Tamamlandı
- İptal edildi

## 18. Dashboard ve Raporlama

Sistem yönetim ekranında şirketin genel durumunu gösterecek dashboard bulunmalıdır.

Dashboard üzerinde örneğin;

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

## 19. Sistem Ana Modülleri

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

## 20. Temel İş Akışı

Sistemin ana operasyon süreci aşağıdaki şekilde olacaktır:

1. Müşteriden taşıma talebi alınması
2. Müşteri / firma bilgilerinin kontrol edilmesi
3. Güzergâh ve yük bilgilerinin girilmesi
4. Müşteriye fiyat verilmesi
5. Teklifin müşteri tarafından onaylanması
6. Şirket filosundaki uygun araçların kontrol edilmesi
7. Uygun araç varsa şirket aracının atanması
8. Uygun araç yoksa taşeron / nakliyeci bulunması
9. Araç ve sürücünün operasyona atanması
10. Araç bilgilerinin müşteriye bildirilmesi
11. İrsaliyenin hazırlanması
12. Yükleme yapılması
13. Taşımanın gerçekleştirilmesi
14. Teslimatın tamamlanması
15. Operasyon evraklarının sisteme eklenmesi
16. Müşteri faturasının hazırlanması
17. Faturanın müşteriye gönderilmesi
18. Faturanın müşteri carisine işlenmesi
19. Taşeron kullanıldıysa taşeron faturasının beklenmesi
20. Taşeron faturasının ilgili operasyona bağlanması
21. Taşeron borcunun cari hesaba işlenmesi
22. Müşteri tahsilatının takip edilmesi
23. Taşeron ödemesinin takip edilmesi
24. Operasyon gelir-giderlerinin hesaplanması
25. Operasyon kârlılığının hesaplanması
26. Operasyonun finansal olarak kapatılması.

## 21. Sistemin Temel Hedefi

Mevcut durumda Excel üzerinde takip edilen operasyon, araç, müşteri, fatura ve finans bilgilerinin birbirinden bağımsız kolonlarda tutulması yerine sistemde tüm kayıtlar ilişkisel olarak tutulacaktır.

Örneğin tek bir operasyon açıldığında sistem üzerinden o operasyonun;

Müşterisi → Yükü → Güzergâhı → Aracı → Sürücüsü → Nakliyecisi → İrsaliyesi → Satış faturası → Gider faturası → Tahsilatı → Ödemesi → Yakıt ve diğer giderleri → Kârlılığı

tek ekran üzerinden takip edilebilmelidir.

Böylece sistem yalnızca bir lojistik iş takip programı değil;

Lojistik Operasyon + CRM + Filo Yönetimi + Finans + Cari Hesap + Ön Muhasebe + Raporlama

fonksiyonlarını tek bir yapı altında birleştiren kapsamlı bir Lojistik ERP sistemi haline gelecektir.
